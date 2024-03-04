using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadIngScene : MonoBehaviour
{
    // Start is called before the first frame update
   

    public void LoadInScene()
    {
        
        SceneManager.LoadScene(SceneValues.earlierScene);
        SceneValues.earlierScene = SceneManager.GetActiveScene().name;
        Debug.Log("detta funkar, 306");
        Debug.Log(SceneValues.earlierScene);
        //Debug.Log("tjo bitch" + SceneValues.positionBeforeBuyShop.position); 

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
