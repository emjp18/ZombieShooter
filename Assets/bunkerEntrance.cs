using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class bunkerEntrance : MonoBehaviour
{
    // Start is called before the first frame update

    public TilemapCollider2D thebunkerRoofHitBox;
   
    public TilemapRenderer theBunkerRoofTopPic;

    public Collider2D thePlayerCollider;

    float time; 


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {



        if (thebunkerRoofHitBox.IsTouching(thePlayerCollider))
        {
            theBunkerRoofTopPic.sortingOrder = 0;

            time += Time.deltaTime;
            if(time < 2)
            {


            }

        }
        else time = 0; 
      
    }
}
