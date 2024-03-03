using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckForItem : MonoBehaviour
{
    [SerializeField] private InventoryManagerScript inventoryScript;
    [SerializeField] private string itemName;
    [SerializeField] private int itemCount;
    [SerializeField] private string nextLevelName;
    public int CountNumberOfItem()
    {
        int currentItemCount = 0;

        for (int i = 0; i < inventoryScript.itemSlot.Length - 1; i++)
        {
            if (inventoryScript.itemSlot[i].itemName == itemName)
            {
                currentItemCount += inventoryScript.itemSlot[i].quantity;
            }
        }

        return currentItemCount;
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("bam    " + CountNumberOfItem());
        if (CountNumberOfItem() >= itemCount)
        {
            Debug.Log("should work");
            SceneManager.LoadScene(nextLevelName);
        }
    }
}
