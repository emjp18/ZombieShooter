[System.Serializable]

public class Item
{
    public string name;
    public int count; //Representing the number of items

    public Item(string itemName, int itemCount)
    {
        name = itemName;
        count = itemCount;
    }
}
