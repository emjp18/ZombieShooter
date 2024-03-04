using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyShopUpgrade : MonoBehaviour
{
    // Start is called before the first frame update
    public void UpgradingFireRate()
    {
        IfCheck(SceneValues.fireRate); 

    }
    public void UpgradingDamage()
    {
        IfCheck(SceneValues.damage); 
    }

    private void IfCheck(int value)
    {

        if( SceneValues.coinsForPlayer >= 10)
        {

            value += 5;

            SceneValues.coinsForPlayer -= 10; 


        }
    }
}
