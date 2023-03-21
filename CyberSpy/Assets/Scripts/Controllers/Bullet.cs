using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed, bulletLife;
    private float timer = 0.0f;

    public Rigidbody myRigidbody;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BulletFlying();

        //bulletLife -=  Time.deltaTime;
        timer += Time.deltaTime;

        if (2 < timer)
        {
            Destroy(gameObject);
            print(timer);

        }
    }

    private void BulletFlying()
    {
        myRigidbody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
