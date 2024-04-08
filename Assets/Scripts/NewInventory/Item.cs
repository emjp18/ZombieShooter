using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
public class Item : MonoBehaviour
{
    [SerializeField]
    public string itemName;

    [SerializeField]
    public int quantity;

    [SerializeField]
    public Sprite itemSprite;

    [SerializeField]
    public float itemDropRate;

    [TextArea]
    [SerializeField]
    public string itemDescription;

    private InventoryManagerScript inventoryManagerScript;

    void Start()
    {
        inventoryManagerScript = GameObject.Find("InventoryCanvas").GetComponent<InventoryManagerScript>();
        if(inventoryManagerScript == null)
        {
            Debug.LogError("No inventory found no no");
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag =="Player")
        {
            int leftOverItems = inventoryManagerScript.AddItem(itemName, quantity, itemSprite, itemDescription);
            if(leftOverItems <= 0)
                Destroy(gameObject);
            else
                quantity = leftOverItems;
            
            
        }
    }
}
