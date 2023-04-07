using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 12.5f, runSpeed = 25f;
    public Vector3 velocity;
    public float gravityModifier;
    public float jumpHeight;
    private bool readyToJump;
    public Transform ground;
    public LayerMask groundLayer;
    public float GroundDistance = 0.5f;
    public Animator myAnimator;

    public bool isRunning= false, startSlideTimer;
    public float currentSliderTimer, maxSlideTime = 2f;
    public float slideSpeed = 30;

    public CharacterController myController;
    public Transform myCameraHead;

    public GameObject bullet;
    public Transform firePosition;

    public GameObject muzzeFlash, bulletHole, goopHole;

    public float mouseSensitivity= 100f;
    private float cameraVerticalRotation;

    private Vector3 crounchScale = new Vector3(1, 0.5f, 1);
    private Vector3 bodyScale;
    public Transform myBody;
    private float initialControllerHeight;
    public float crounchSpeed = 6f;
    public bool isCrounching = false;

    private void Start()
    {
        bodyScale = myBody.localScale;
        initialControllerHeight = myController.height;

    }
    void Update()
    {
        PlayerMovement();
        CameraMovement();
        Jump();
        Shoot();
        Crounching();
        SlideCounter();
    }

    private void Crounching()
    {
        if (Input.GetKeyDown(KeyCode.C))
            StartCrouching();

        if (Input.GetKeyUp(KeyCode.C) || currentSliderTimer > maxSlideTime)
            StopCouching();
    }



    private void StartCrouching()
    {
        myBody.localScale = crounchScale;
        myCameraHead.position -= new Vector3(0, 0.7f, 0);

        myController.height /= 2;
        isCrounching = true;

        if (isRunning)
        {
            velocity = Vector3.ProjectOnPlane(myCameraHead.transform.forward, Vector3.up).normalized * slideSpeed * Time.deltaTime;
            startSlideTimer = true;
        }
    }
    private void StopCouching()
    {
        currentSliderTimer = 0f;
        velocity = new Vector3(0f, 0f, 0f);
        startSlideTimer = false;

        myBody.localScale = bodyScale;
        myCameraHead.position += new Vector3(0, 0.7f, 0);

        myController.height = initialControllerHeight;
        isCrounching = false;
    }
    public void Jump()
    {
        readyToJump = Physics.OverlapSphere(ground.position, GroundDistance, groundLayer).Length > 0;

        if (Input.GetButtonDown("Jump") && readyToJump)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y) * Time.deltaTime;
        }

        myController.Move(velocity);
    }

    public void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(myCameraHead.position, myCameraHead.forward, out hit, 100f))
            {
                if(Vector3.Distance(myCameraHead.position, hit.point) > 2f)
                {
                    firePosition.LookAt(hit.point);

                    if(hit.collider.tag == "Shootable")
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

            Instantiate(muzzeFlash, firePosition.position, firePosition.rotation, firePosition);
            Instantiate(bullet, firePosition.position, firePosition.rotation);
        }
    }

    private void CameraMovement()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        cameraVerticalRotation -= mouseY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);

        transform.Rotate(Vector3.up * mouseX);
        myCameraHead.localRotation = Quaternion.Euler(cameraVerticalRotation, 0f, 0f);
    }

    private void PlayerMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = x * transform.right + z * transform.forward;

        if (Input.GetKey(KeyCode.LeftShift) && !isCrounching)
        {
            movement = movement * runSpeed * Time.deltaTime;
            isRunning = true;
        }

        else if (isCrounching)
        {
            movement = movement * crounchSpeed * Time.deltaTime;
        } 
        else 
        {
            movement = movement * speed * Time.deltaTime;
            isRunning = false;

        }

        myAnimator.SetFloat("PlayerSpeed", movement.magnitude);
        Debug.Log(movement.magnitude);

        myController.Move(movement);

        velocity.y += Physics.gravity.y * Mathf.Pow(Time.deltaTime, 2) * gravityModifier;

        myController.Move(velocity);

        if (myController.isGrounded)
            velocity.y = Physics.gravity.y * Time.deltaTime;
    }

    private void SlideCounter()
    {
        if (startSlideTimer)
        {
            currentSliderTimer += Time.deltaTime;
        }
    }
}
