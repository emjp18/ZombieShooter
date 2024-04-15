using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayMoney : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    
    // Update is called once per frame
    void Update()
    {
        moneyText.text = "Money: " + SceneValues.coinsForPlayer;
    }
}
