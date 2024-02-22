using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public GameObject bullet;

    private CameraMovement cameraMovement;

    private float spawnOffset;

    public float reloadTime;
    private float reloadTimer;

    public int damage;
    public float range;

    void Start()
    {
        cameraMovement = Camera.main.GetComponent<CameraMovement>();

        //Calculates the offset needed so that the bullet can get cloned at its edge not center (to avoid the bullet overlapping with the player when its being fired)
        spawnOffset = (bullet.transform.lossyScale.y / 2);
    }

    void Update()
    {
        reloadTimer += Time.deltaTime;
        if (Input.GetMouseButtonDown(0) && reloadTimer >= reloadTime)
        {
            //Clones a bullet at position of the gun + offset * transform.up to the offset independent of rotation, and then sets it active
            GameObject bulletInstance = Instantiate(bullet, gameObject.transform.position + spawnOffset * transform.up, gameObject.transform.rotation);
            bulletInstance.SetActive(true);
            Bullet bulletScript = bulletInstance.GetComponent<Bullet>();
            bulletScript.damage = damage;
            bulletScript.timerUntilDestoyed = range/bulletScript.speed;

            reloadTimer = 0;
            cameraMovement.shakeTimer = 0;
        }
    }
}
