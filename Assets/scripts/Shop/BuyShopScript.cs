using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class BuyShopScript : MonoBehaviour
{
    // Start is called before the first frame update

    public TilemapCollider2D theCollider;
    public CircleCollider2D thePlayerCollider;
    public Transform thePlayerPosition;
    bool touching; 
    bool touchedBefore; 
    void Start()
    {
    
        touching = true; 
       
    }

    // Update is called once per frame
    void Update()
    {
        touchedBefore = touching; 
        touching = theCollider.IsTouching(thePlayerCollider);
        // Debug.Log(touching); 

        if (touching && !touchedBefore)
        {
            // Debug.Log("Funkar"); 
            SceneValues.earlierScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene("BuyShopScene");
            SceneValues.positionBeforeBuyShop = (thePlayerPosition.position.x, thePlayerPosition.position.y, thePlayerPosition.position.z ); 
           
        }
       
       
    }
}
