using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance; //will make the Inventory class accessable from other classes
    public List<Item> items = new List<Item>(); //A list to hold the items

    private void Awake() //Checks so that there can only be a single instance of the inventory class at a time
    {
        //DontDestroyOnLoad(gameObject);
        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
    }
    public void AddItem(Item itemToAdd)
    {
        bool itemExists = false; //A check to see if an item exists

        foreach(Item item in items)
        {
            if(item.name == itemToAdd.name) //Checks if the name of the item is the same as itemToAdd (which it will be because the method has itemToAdd as a parameter
            {
                item.count += itemToAdd.count; //Adds the count in the inventory by 1 for each item
                itemExists = true; //Sets itemExists to true 
                break; //Breaks the loop
            }
        }
        if (!itemExists) //Checks if the bool itemExists is still false, then the item deos not exist in the inventory
        {
            items.Add(itemToAdd); //Which adds the item to the itemlist
        }
        //Debug.Log(itemToAdd.count + " " + itemToAdd.name + "Added to inventory");
    }
    public void RemoveItem(Item itemToRemove)
    {
        //List<Item> itemsToRemove = new List<Item>();

        foreach (var item in items)
        {
            if(item.name == itemToRemove.name) //
            {
                item.count -= itemToRemove.count; //
                if(item.count <= 0)
                {
                    items.Remove(item);
                }
                break;
            }
        }
        //foreach(var item in itemsToRemove)
        //{
        //    items.Remove(item);
        //}

        Debug.Log(itemToRemove.count + " " + itemToRemove.name + "removed from inventory");
    }

}
