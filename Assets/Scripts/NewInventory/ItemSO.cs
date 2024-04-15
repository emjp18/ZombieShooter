using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemSO : ScriptableObject
{
    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;


    public AmmoToChange ammoToChange = new AmmoToChange();
    public int amountToChangeAmmo;


    public void UseItem()
    {
        if(statToChange == StatToChange.health)
        {
            GameObject.Find("Player").GetComponent<Health>().Heal(amountToChangeStat);
            
        }
    }




    public enum StatToChange
    {
        none,
        health
    };
    public enum AmmoToChange
    {
        none,
        handGunAmmo,
        rifleAmmo,
        flameThrowerAmmo
    }
}
