using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBag : MonoBehaviour
{
    public GameObject Itemprefab;
    public List<Item> itemList = new List<Item>();

    public void Start()
    {

    }
    Item GetDroppedItem() //Randomly rolling for the loot 
    {
        int randomNumber = Random.Range(1, 101);
        List<Item> possibleItems = new List<Item>();
        foreach(Item item in itemList)
        {
            if(randomNumber <= item.itemDropRate)
            {
                possibleItems.Add(item);

            }
        }
        if (possibleItems.Count > 0) //If the random number is higher or lower than all the items dropchance, check for the next best thing
        {
            Item droppedItem = possibleItems[Random.Range(0, possibleItems.Count)];
            return droppedItem;
        }
        return null; //If nothing gets dropped
    }
    public void InstatiateLoot(Vector3 spawnPosition)
    {
        Item droppedItem = GetDroppedItem();
        if(droppedItem != null)
        {
            GameObject ItemGameObject = Instantiate(Itemprefab, spawnPosition, Quaternion.identity);
            ItemGameObject.GetComponent<SpriteRenderer>().sprite = droppedItem.itemSprite;
        }
    }
}
