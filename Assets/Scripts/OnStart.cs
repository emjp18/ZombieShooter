using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnStart : MonoBehaviour
{
    void Start()
    {
        SceneValues.coinsForPlayer = PlayerPrefs.GetInt("Coins");
    }
}
