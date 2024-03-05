using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{ 
    public Explode explodeScript;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            explodeScript.isExploding = true;
        }
    }
}
