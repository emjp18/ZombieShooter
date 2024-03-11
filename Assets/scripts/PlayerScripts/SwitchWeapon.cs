using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchWeapon : MonoBehaviour
{
    [SerializeField] private GameObject Gun;
    [SerializeField] private GameObject Rifle;
    [SerializeField] private GameObject FlameThrower;
    [SerializeField] private GameObject Knife;
    [SerializeField] private GameObject Bat;

    /* USED FOR PLAYER MODEL ANIMATION */
    public Animator playerAnimator;

    void Start()
    {
        playerAnimator.SetInteger("WeaponType", 1);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Gun.SetActive(true);
            Rifle.SetActive(false);
            FlameThrower.SetActive(false);
            Knife.SetActive(false);
            Bat.SetActive(false);
            playerAnimator.SetInteger("WeaponType", 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Gun.SetActive(false);
            Rifle.SetActive(true);
            FlameThrower.SetActive(false);
            Knife.SetActive(false);
            Bat.SetActive(false);
            playerAnimator.SetInteger("WeaponType", 2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Gun.SetActive(false);
            Rifle.SetActive(false);
            FlameThrower.SetActive(true);
            Knife.SetActive(false);
            Bat.SetActive(false);
            playerAnimator.SetInteger("WeaponType", 3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Gun.SetActive(false);
            Rifle.SetActive(false);
            FlameThrower.SetActive(false);
            Knife.SetActive(true);
            Bat.SetActive(false);
            playerAnimator.SetInteger("WeaponType", 3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Gun.SetActive(false);
            Rifle.SetActive(false);
            FlameThrower.SetActive(false);
            Knife.SetActive(false);
            Bat.SetActive(true);
            playerAnimator.SetInteger("WeaponType", 3);
        }
    }
}
