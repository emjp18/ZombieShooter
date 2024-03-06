using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    public float impactField;
    public float force;
    public LayerMask lmToHit;
    public GameObject explosionPrefab;
    public int damage = 10;
    public float explosionDelay = 1f;
    private float explosionTimer = 0f;
    public bool isExploding = false;

    void Update()
    {

        if (isExploding)
        {
            explosionTimer += Time.deltaTime;
            if (explosionTimer >= explosionDelay)
            {
                Explosion();
            }
        }
    }

    void Explosion()
    {
        Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, impactField, lmToHit);
        foreach (Collider2D obj in objects)
        {
            Vector2 dir = obj.transform.position - transform.position;

            obj.GetComponent<Rigidbody2D>().AddForce(dir * force);
        }
        float offset = explosionPrefab.transform.localScale.x*4;
        // Instantiate the explosion at the bomb's position with the offset
        Instantiate(explosionPrefab, transform.position + new Vector3(offset, 0, 0), Quaternion.identity);
        Destroy(gameObject);
        //===========Deal Damage===========
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, impactField);
    }

}
