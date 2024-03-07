using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisPlayCoinsScript : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI theText;


    public void Start()
    {
        theText.text = $"Coins: {SceneValues.coinsForPlayer}";
    }

    public void ChangingText()
    {
        theText.text = $"Coins: {SceneValues.coinsForPlayer}"; 
    }
}
