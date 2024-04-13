using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieHealth : MonoBehaviour
{
    [SerializeField] private int healthPoints;

    private Rigidbody2D rigidBody;
    private ZombieMovement zombieMovementScript;

    [SerializeField] private GameObject coinPrefab;
    private GameObject corpseObject;



    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        zombieMovementScript = gameObject.GetComponent<ZombieMovement>();

        //The first child of the zombie should be its corpse
        corpseObject = transform.GetChild(0).gameObject;
    }

    private void TakeDamage(int damageTaken)
    {
        healthPoints -= damageTaken;

        if (healthPoints <= 0)
        {
            //Spawns the zombies corpse at the zombies position. Then sets the corpse to active
            //Since the corpse is a child of the zombie it will already be rotated correctly
            Instantiate(corpseObject, gameObject.transform.position, corpseObject.transform.rotation).SetActive(true);

            //Spawns a random amount of coins between 1-3 around the zombie with a small random offset
            for (int i = 0; i < Random.Range(1, 4); i++)
            {
                Vector3 randomOffset = new Vector3((Random.Range(0f, 2f) - 1f) / 4, (Random.Range(0f, 2f) - 1f) / 4, 0);
                Instantiate(coinPrefab, gameObject.transform.position + randomOffset, gameObject.transform.rotation);
            }

            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collisionGameObject = collision.gameObject;

        if (collisionGameObject.tag == "bullet")
        {
            int damage = collisionGameObject.GetComponent<Bullet>().damage;

            TakeDamage(damage);

            //Makes the zombie get knockback
            Vector2 vectorFromBullet = gameObject.transform.position- collisionGameObject.transform.position ;
            zombieMovementScript.TakeKnockBack(vectorFromBullet.normalized);
        }
    }
}
