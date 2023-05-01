using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerhealthSystem : MonoBehaviour
{
    public int maxHealth;
    public int currentHealth;

    UICanvasController healthbar;

    void Start()
    {
        currentHealth = maxHealth;
        healthbar = FindObjectOfType<UICanvasController>();
        healthbar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        healthbar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            //print("ok you hit me");
        }
    }
}
