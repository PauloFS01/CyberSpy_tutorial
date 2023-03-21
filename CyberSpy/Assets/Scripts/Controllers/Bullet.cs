using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    public Rigidbody myRigidbody;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        BulletFlying();
    }

    private void BulletFlying()
    {
        myRigidbody.velocity = transform.forward * speed;
    }
}
