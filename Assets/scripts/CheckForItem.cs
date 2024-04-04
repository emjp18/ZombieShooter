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

    [SerializeField] private GameObject InventorySlots;//Should get the parent of all the ItemSlots in the inventoryCanvas

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
        if (collision.gameObject.tag == "Player")
        {
            if (CountNumberOfItem() >= itemCount)
            {
                /*PlayerPrefs.SetInt("Coins", SceneValues.coinsForPlayer);

                for (int i = 0; i < InventorySlots.transform.childCount; i++)
                {
                    ItemSlot itemSlot = InventorySlots.transform.GetChild(i).GetComponent<ItemSlot>();

                    PlayerPrefs.SetString("ItemName" + i, itemSlot.itemName);
                    PlayerPrefs.SetInt("ItemQuantity" + i, itemSlot.quantity);
                    PlayerPrefs.SetString("ItemDescription" + i, itemSlot.itemDescription);
                }*/

                SceneManager.LoadScene(nextLevelName);
            }
        }
    }
}
