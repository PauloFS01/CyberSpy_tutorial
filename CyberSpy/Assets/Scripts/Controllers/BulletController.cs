using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{

    public float speed, bulletLife;
    public Rigidbody myRigidBody;
    private float timer = 0.0f;
    void Start()
    {
        
    }

    void Update()
    {
        BulletFly();

        timer += Time.deltaTime;

        if (2 < timer)
        {
            Destroy(gameObject);

        }
    }

    private void BulletFly()
    {
        myRigidBody.velocity = transform.forward * speed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
