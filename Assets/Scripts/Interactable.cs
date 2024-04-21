using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    private bool inRange = false;

    [SerializeField] private bool payToOpen;
    [SerializeField] private int cost;

    [SerializeField] private bool toNextLevel;
    [SerializeField] private string nextLevelName;

    [SerializeField] private bool useItemForNextLevel;
    [SerializeField] private InventoryManagerScript inventoryScript;
    [SerializeField] private string itemName;
    [SerializeField] private int itemCount;
    [SerializeField] private GameObject InventorySlots;//Should get the parent of all the ItemSlots in the inventoryCanvas


    private void Update()
    {
        if (inRange && Input.GetKeyDown(KeyCode.E))
        {
            if (payToOpen)
            {
                PayToOpen();
            }
            else if (toNextLevel)
            {
                GoToNextLevel();
            }
            else if (useItemForNextLevel)
            {
                UseItemToNextLevel();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
        }
    }

    private void PayToOpen()
    {
        if (OnStart.coins >= cost)
        {
            OnStart.coins -= cost;
            Destroy(gameObject);
        }
    }

    private void GoToNextLevel()
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

    private void UseItemToNextLevel()
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
}
