using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public Transform crosshairPosition;

    private float timerUntilDestoyed;
    private Vector2 direction;
    private float speed;

    void Start()
    {
        //Normalizes the vector from the bullets position to the crosshairs position, to create a vector representing the direction the bullet should move
        direction = (crosshairPosition.position - transform.position).normalized;

        timerUntilDestoyed = 0.5f;
        speed = 40;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        timerUntilDestoyed -= Time.deltaTime;
        if (timerUntilDestoyed <= 0)
        {
            Destroy(gameObject);
        }
        Debug.Log(timerUntilDestoyed);
    }
}
