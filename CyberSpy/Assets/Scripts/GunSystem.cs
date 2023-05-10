using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    public Transform myCameraHead;
    public Transform firePosition;

    public Animator myAnimator;

    private UICanvasController myUICanvas;

    public GameObject muzzeFlash, bulletHole, goopHole, bloodEffect;
    public GameObject bullet;

    public bool canAutoFire;
    private bool shooting, readyToShoot = true;
    public float timeBetweenShots;

    public int bulletsAvaiable, totalBullets, magazineSize;

    private bool reloading = false;
    public float reloadTime=3f;

    // aiming
    public Transform aimPosition;
    private float aimSpeed=2f;
    private Vector3 gunStartPosition;
    public float zoomAmount;

    public int damageAmount;

    public string gunName;

    public bool isARocketLauch;

    public int pickUpBulletAmmount;

    void Start()
    {
        totalBullets -= magazineSize;
        bulletsAvaiable = magazineSize;
        gunStartPosition = transform.localPosition; //local position is a refence to the parent position

        myUICanvas = FindObjectOfType<UICanvasController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        GunManger();
        UpdateAmmoText();
    }

    private void GunManger()
    {
        if (Input.GetKeyDown(KeyCode.R) && bulletsAvaiable < magazineSize && !reloading)
            Reload();

        if (Input.GetMouseButton(1))
            transform.position = Vector3.MoveTowards(transform.position, aimPosition.position, aimSpeed * Time.deltaTime);
        else
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, gunStartPosition, aimSpeed * Time.deltaTime);

        if (Input.GetMouseButtonDown(1))
            FindObjectOfType<CameraMove>().ZoomIn(zoomAmount);
        if (Input.GetMouseButtonUp(1))
            FindObjectOfType<CameraMove>().ZoomOut();
    }

    public void Shoot()
    {
        if (canAutoFire)
        {
            shooting = Input.GetMouseButton(0);
        }
        else
        {
            shooting = Input.GetMouseButtonDown(0);
        }


        if (shooting && readyToShoot && bulletsAvaiable > 0 && !reloading)
        {
            readyToShoot = false;
            RaycastHit hit;
            if (Physics.Raycast(myCameraHead.position, myCameraHead.forward, out hit, 100f))
            {
                if (Vector3.Distance(myCameraHead.position, hit.point) > 2f)
                {
                    firePosition.LookAt(hit.point);
                    if (!isARocketLauch)
                    {
                    if (hit.collider.tag == "Shootable")
                        Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));

                    if (hit.collider.tag == "GoopLeaker")
                        Instantiate(goopHole, hit.point, Quaternion.LookRotation(hit.normal));
                    }

                }
                if (hit.collider.CompareTag("Enemy") && !isARocketLauch)
                {
                    hit.collider.GetComponent<EnemyHealthSystem>().TakeDamage(damageAmount);
                    Instantiate(bloodEffect, hit.point, Quaternion.LookRotation(hit.normal));
                }
            }
            else
            {
                firePosition.LookAt(myCameraHead.position + (myCameraHead.forward * 50f));
            }

            bulletsAvaiable--;

            if (!isARocketLauch)
            {
                Instantiate(muzzeFlash, firePosition.position, firePosition.rotation, firePosition);
                Instantiate(bullet, firePosition.position, firePosition.rotation, firePosition);
            }
            else
            {
                Instantiate(bullet, firePosition.position, firePosition.rotation);
            }



            StartCoroutine(ResetShot());

        }
    }

    public void Reload()
    {

        if(gunName == "Pistol") myAnimator.SetTrigger("PistolReload");

        AudioManager.instance.PlayerSFX(7);

        reloading = true;
        StartCoroutine(ReloadTime());
    }

    private void UpdateAmmoText()
    {
        myUICanvas.ammoText.SetText(bulletsAvaiable + "/" + magazineSize);
        myUICanvas.totalAmmoText.SetText("Total:" + totalBullets);
    }

    public void AddAmmonition()
    {
        totalBullets += pickUpBulletAmmount;
    }

    IEnumerator ResetShot()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        readyToShoot = true;
    }

    IEnumerator ReloadTime()
    {
        yield return new WaitForSeconds(reloadTime);

        int bulletsToAdd = magazineSize - bulletsAvaiable;

        if (totalBullets > bulletsToAdd)
        {
            totalBullets -= bulletsToAdd;
            bulletsAvaiable = magazineSize;
        }
        else
        {
            bulletsAvaiable += totalBullets;
            totalBullets = 0;
        }
        reloading = false;
    }
}
