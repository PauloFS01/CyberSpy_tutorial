using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public int amountOfHealing = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.PlayerSFX(1);
            other.GetComponent<PlayerhealthSystem>().HealPlayer(amountOfHealing);
            Destroy(gameObject);
        }
    }
}
