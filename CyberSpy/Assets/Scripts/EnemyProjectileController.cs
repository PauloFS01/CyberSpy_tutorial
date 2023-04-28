using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectileController : MonoBehaviour
{
    Rigidbody myRigidBody;
    public float upForce, forwadForce;
    public int damageAmount;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody>();

        GrenadeThrow();
    }

    private void GrenadeThrow()
    {
        myRigidBody.AddForce(transform.forward * forwadForce, ForceMode.Impulse);
        myRigidBody.AddForce(transform.up * upForce, ForceMode.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<PlayerhealthSystem>().TakeDamage(damageAmount);
        }
    }
}
