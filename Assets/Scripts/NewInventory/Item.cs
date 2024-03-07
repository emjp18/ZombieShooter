using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
public class Item : MonoBehaviour
{
    [SerializeField]
    private string itemName;

    [SerializeField]
    private int itemCount;

    [SerializeField]
    private Sprite itemSprite;

    [TextArea]
    [SerializeField]
    private string itemDescription;

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
            int leftOverItems = inventoryManagerScript.AddItem(itemName, itemCount, itemSprite, itemDescription);
            if(leftOverItems <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                itemCount = leftOverItems;
            }
            
        }
    }
}
