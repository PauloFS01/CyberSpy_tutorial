using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerhealthSystem : MonoBehaviour
{
    public int currentHealth = 5;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            //print("ok you hit me");
        }
    }
}
