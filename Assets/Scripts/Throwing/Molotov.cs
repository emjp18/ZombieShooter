using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Molotov : MonoBehaviour
{
    public BurnGround burnGroundScript;
    public Throw throwScript;

    void Update()
    {

        if (throwScript.throwing)
        {

            burnGroundScript.isBurning = true;

        }

    }

}
