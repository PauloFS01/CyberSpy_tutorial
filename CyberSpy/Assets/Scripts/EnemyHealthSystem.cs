using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthSystem : MonoBehaviour
{
    public int maxHealth;
    private int currentHealth;


    EnemyUICanvasController healthBar;
    void Start()
    {
        healthBar = GetComponentInChildren<EnemyUICanvasController>();
        healthBar.SetMaxHealth(maxHealth);

        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth-= damageAmount;
        healthBar.SetHealth(currentHealth);
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
            //print("ok you hit me");
        }
    }
}
