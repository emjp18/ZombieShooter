using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseWeapon : MonoBehaviour
{
    float fireDelay;
    float reloadTime;

    int ammoCapacity;
    int ammoCount;

    protected virtual void WeaponAttack() { }

    protected virtual void Reload() { }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
