using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangingSizeWithCamScript : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject camera;

    public RectTransform theBackGround; 
    void Start()
    {
       
      
        //float cameraHeight = Camera.main.orthographicSize * 2f;
        //float cameraWidth = cameraHeight * Camera.main.aspect;
        //theBackGround.sizeDelta.Set(cameraWidth, cameraHeight);

      
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }
}
