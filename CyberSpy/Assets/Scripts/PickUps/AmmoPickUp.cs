using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponentInChildren<GunSystem>().AddAmmonition();

            AudioManager.instance.PlayerSFX(1);

            gameObject.SetActive(false);
        }
    }
}
