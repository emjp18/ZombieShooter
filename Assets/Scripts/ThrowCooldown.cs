using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class ThrowCooldown : MonoBehaviour
{
    public bool canThrow = true;

    // Method to set canThrow to false for 0.5 seconds
    public void SetThrowCooldown()
    {

        canThrow = false;
        StartCoroutine(ResetThrowCooldown());
        
    }

    // Coroutine to reset canThrow after 0.5 seconds
    IEnumerator ResetThrowCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        canThrow = true;
    }
}
