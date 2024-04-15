//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class PickUp : MonoBehaviour
//{
//    public Item item; /*= new Item("Item Name", 1);*/


//    public void PickUpItem()
//    {
//        if (item != null)
//        {
//            Inventory.instance.AddItem(item);
//            Destroy(gameObject);
//        }
//    }
//    void OnTriggerEnter2D(Collider2D collision)
//    {
//        if (collision.CompareTag("Player"))
//        {
//            PickUpItem();
//        }
//    }
//}
