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

    private InventoryManagerScript inventoryManagerScript;

    void Start()
    {
        inventoryManagerScript = GameObject.Find("Inventory").GetComponent<InventoryManagerScript>();
        if(inventoryManagerScript == null)
        {
            Debug.LogError("No inventory found no no");
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Item has collided with player");
            inventoryManagerScript.AddItem(itemName, itemCount, itemSprite);
            Destroy(gameObject);
        }
    }
}
