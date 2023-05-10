using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsPickup : MonoBehaviour
{
    public string gunPickupName;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.PlayerSFX(6);
            other.gameObject.GetComponentInChildren<WeaponsSwitSystem>().AddGun(gunPickupName);
            gameObject.SetActive(false);
        }
    }
}
