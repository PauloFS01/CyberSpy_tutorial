using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsSwitSystem : MonoBehaviour
{
    GunSystem activeGun;
    public List<GunSystem> listOfGuns = new List<GunSystem>();
    public int currentGun=0;
    void Start()
    {
        listOfGuns[currentGun].gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SwitchGun();
        }
    }

    private void SwitchGun()
    {
       foreach(GunSystem gun in listOfGuns)
        {
            gun.gameObject.SetActive(false);
        }
        currentGun++;
        if(currentGun >= listOfGuns.Count)
        {
            currentGun = 0;
        }

        listOfGuns[currentGun].gameObject.SetActive(true);
    }
}
