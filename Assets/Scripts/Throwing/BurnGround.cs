using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BurnGround : MonoBehaviour
{
    public float impactField;
    public LayerMask lmToHit;
    public GameObject burningPrefab;
    public int damage = 2;
    public float igniteDelay = 1f;
    private float timer = 0f;
    float interval = 2f;
    public bool isBurning = false;
    private GameObject burningEffect;


    void Update()
    {

        if (isBurning)
        {
            timer += Time.deltaTime;
            if (timer >= igniteDelay)
            {
                IgniteGround();
            }
        }
    }

    void IgniteGround()
    {
        StartCoroutine(PlayEffectRepeatedly(1f, 5f));
    }

    IEnumerator PlayEffectRepeatedly(float delay, float duration)
    {
        float startTime = Time.time;
        float endTime = startTime + duration;

        while (Time.time < endTime)
        {
            float nextEffectTime = startTime + Mathf.Ceil((Time.time - startTime) / delay) * delay;
            yield return new WaitForSeconds(nextEffectTime - Time.time);
            PlayEffect();

        }

        // Destroy the object after the duration
        Destroy(gameObject);
    }

    void PlayEffect()
    {
        // Spawn burning prefab and play animation
        if (burningEffect == null)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            rb.freezeRotation = true;
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            sr.sprite = null;

            float offset = burningPrefab.transform.localScale.x * 2.5f;
            burningEffect = Instantiate(burningPrefab, transform.position + new Vector3(offset, 0, 0), Quaternion.identity);
            burningEffect.transform.SetParent(transform);

            // Adjust alpha value of the material to make it slightly transparent
            Renderer[] renderers = burningEffect.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                foreach (Material material in renderer.materials)
                {
                    Color color = material.color;
                    color.a = 0.1f; // Set alpha value here (0 is fully transparent, 1 is fully opaque)
                    material.color = color;
                }
            }
        }
    }



    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, impactField);
    }

    // You may want to add logic to handle collision/trigger detection to start burning
}
