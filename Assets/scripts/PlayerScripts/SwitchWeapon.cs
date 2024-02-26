using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapon : MonoBehaviour
{
    [SerializeField] private GameObject Gun;
    [SerializeField] private GameObject Rifle;
    [SerializeField] private GameObject FlameThrower;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Gun.SetActive(true);
            Rifle.SetActive(false);
            FlameThrower.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Gun.SetActive(false);
            Rifle.SetActive(true);
            FlameThrower.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Gun.SetActive(false);
            Rifle.SetActive(false);
            FlameThrower.SetActive(true);
        }
    }
}
