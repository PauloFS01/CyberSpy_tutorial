using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSystem : MonoBehaviour
{
    public Transform myCameraHead;
    public Transform firePosition;

    public GameObject muzzeFlash, bulletHole, goopHole;
    public GameObject bullet;

    public bool canAutoFire;
    private bool shooting, readyToShoot = true;
    public float timeBetweenShots;

    public int bulletsAvaiable, totalBullets, magazineSize;

    private bool reloading = false;
    public float reloadTime=3f;

    void Start()
    {
        totalBullets -= magazineSize;
        bulletsAvaiable = magazineSize;
        
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        GunManger();
    }

    private void GunManger()
    {
        if (Input.GetKeyDown(KeyCode.R) && bulletsAvaiable < magazineSize && !reloading)
        {
//            StartCoroutine(ReloadTime());
            Reload();
        }
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

                    if (hit.collider.tag == "Shootable")
                        Instantiate(bulletHole, hit.point, Quaternion.LookRotation(hit.normal));

                    if (hit.collider.tag == "GoopLeaker")
                        Instantiate(goopHole, hit.point, Quaternion.LookRotation(hit.normal));
                }
                if (hit.collider.CompareTag("Enemy"))
                    Destroy(hit.collider.gameObject);
            }
            else
            {
                firePosition.LookAt(myCameraHead.position + (myCameraHead.forward * 50f));
            }

            bulletsAvaiable--;

            Instantiate(muzzeFlash, firePosition.position, firePosition.rotation, firePosition);
            Instantiate(bullet, firePosition.position, firePosition.rotation, firePosition);

            StartCoroutine(ResetShot());

        }
    }

    public void Reload()
    {
        int bulletsToAdd = magazineSize - bulletsAvaiable;

        if(totalBullets > bulletsToAdd)
        {
            totalBullets -= bulletsToAdd;
            bulletsAvaiable = magazineSize;
        }
        else
        {
            bulletsAvaiable += totalBullets;
            totalBullets = 0;
        }
        reloading = true;
        StartCoroutine(ReloadTime());
    }

    IEnumerator ResetShot()
    {
        yield return new WaitForSeconds(timeBetweenShots);
        readyToShoot = true;
    }

    IEnumerator ReloadTime()
    {
        yield return new WaitForSeconds(reloadTime);
        reloading = false;
    }
}
