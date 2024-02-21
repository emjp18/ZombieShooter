using System.Collections;
using System.Collections.Generic;
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
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            inventoryManagerScript.AddItem(itemName, itemCount, itemSprite);
            Destroy(gameObject);
        }
        if(Input.GetKeyDown(KeyCode.E))
        {
            Destroy(gameObject);
        }
    }
}
