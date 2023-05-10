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
        if (currentHealth > maxHealth) currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        AudioManager.instance.PlayerSFX(5);

        healthbar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);

            AudioManager.instance.StopBackgroundMusic();
            AudioManager.instance.PlayerSFX(4);

            FindObjectOfType<GameManager>().PlayerRespawn();
        }
    }
    public void HealPlayer(int amountOfHelth)
    {
        currentHealth += amountOfHelth;
    }
}
