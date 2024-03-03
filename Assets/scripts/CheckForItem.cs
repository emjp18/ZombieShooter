using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckForItem : MonoBehaviour
{
    [SerializeField] private InventoryManagerScript inventoryScript;
    [SerializeField] string itemName;
    [SerializeField] int itemCount;

    int currentItemCount;

    void Update()
    {
        if (currentItemCount < itemCount)
        {
            currentItemCount = 0;
            for (int i = 0; i < inventoryScript.itemSlot.Length - 1; i++)
            {
                if (inventoryScript.itemSlot[i].itemName == itemName)
                {
                    currentItemCount += inventoryScript.itemSlot[i].quantity;
                }
            }
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (currentItemCount >= itemCount)
        {
            //Add win code here
            Debug.Log("car on!     " + currentItemCount);
        }
    }
}
