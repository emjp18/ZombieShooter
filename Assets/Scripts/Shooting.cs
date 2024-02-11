using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;
    public float reloadTime;
    private float reloadTimer;

    void Start()
    {

    }

    void Update()
    {
        reloadTimer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && reloadTimer >= reloadTime)
        {
            //Clones a bullet at the position of the object the script is on(the gun) and then sets it active
            GameObject bulletInstance = Instantiate(bullet, gameObject.transform.position, Quaternion.identity);
            bulletInstance.SetActive(true);

            reloadTimer = 0;
        }
    }
}
