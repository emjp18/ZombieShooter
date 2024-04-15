using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public GameObject mainInventory; //Reference to the MainInventory GameObject
    public GameObject darkBackGround;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (mainInventory != null)
            {
                mainInventory.SetActive(!mainInventory.activeSelf);
            }
            if(darkBackGround != null)
            {
                darkBackGround.SetActive(!darkBackGround.activeSelf);
            }
        }
    }
}
