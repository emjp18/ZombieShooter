using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{ 
    public Explode explodeScript;
    public Throw throwScript;

    void Update()
    {
        if (Input.GetKeyDown(throwScript.actionKey))
        {
            explodeScript.isExploding = true;
        }
    }
}
