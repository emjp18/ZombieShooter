using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpCoin : MonoBehaviour
{
    [SerializeField] int coinsWorth;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SceneValues.coinsForPlayer += coinsWorth;
            Debug.Log(SceneValues.coinsForPlayer);
            Destroy(gameObject);
        }
    }
}
