using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
public class ActiveItem : MonoBehaviour
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
    public bool inInventory = false;

    void Start()
    {
        inventoryManagerScript = GameObject.Find("InventoryCanvas").GetComponent<InventoryManagerScript>();
        if (inventoryManagerScript == null)
        {
            Debug.LogError("No inventory found no no");
        }
    }
    void Update()
    {
        if(inInventory)
        {
            GetComponent<Renderer>().enabled = false;
        }
        else
        {
            GetComponent<Renderer>().enabled = true;
        }
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("Item has collided with player");
            inventoryManagerScript.AddItem(itemName, itemCount, itemSprite, itemDescription);
            
            inInventory = true;
        }
    }
}
