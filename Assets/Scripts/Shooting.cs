using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject bulletInstance = Instantiate(bullet, gameObject.transform.position, Quaternion.identity);
            bulletInstance.SetActive(true);
        }
    }
}
