using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform crosshairTransform;

    private float timerUntilDestoyed;
    private Vector2 direction;
    public float speed;

    void Start()
    {
        //Normalizes the vector from the bullets position to the crosshairs position, to create a vector representing the direction the bullet should move
        direction = (crosshairTransform.position - transform.position).normalized;

        timerUntilDestoyed = 0.5f;
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        timerUntilDestoyed -= Time.deltaTime;
        if (timerUntilDestoyed <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Collision (Wall)")
        {
            Destroy(gameObject);
        }
    }
}
