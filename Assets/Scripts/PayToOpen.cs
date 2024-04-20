using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PayToOpen : MonoBehaviour
{
    [SerializeField] private int cost;

    private bool inRange = false;

    private void Update()
    {
        if (inRange)
        {
            if (Input.GetKeyDown(KeyCode.E) && OnStart.coins >= cost)
            {
                OnStart.coins -= cost;
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inRange = false;
        }
    }
}
