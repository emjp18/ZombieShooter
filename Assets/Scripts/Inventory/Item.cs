using UnityEngine;

[System.Serializable]

public class Item
{
    public string name;
    public int count; //Representing the number of items
    public GameObject prefab;

    public Item(string itemName, int itemCount)
    {
        name = itemName;
        count = itemCount;
    }
}
