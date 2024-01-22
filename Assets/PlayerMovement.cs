using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float speed;
    Vector3 direction;

    void Start()
    {
        speed = 1.5f;
        direction = Vector3.zero;
    }

    void Update()
    {
        //GetAxis() returns a value of -1, 0 or 1 depending on button clicked, Which button does what can be seen under "input manager" in project settings
        //Its normalized so that the speed will be consistent even if you are walking diagonaly
        direction = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0).normalized;
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
