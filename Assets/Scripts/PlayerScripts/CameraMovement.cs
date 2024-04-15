using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform playerTransform;

    private Vector3 newPosition;

    private float temporaryXOffset;
    private float temporaryYOffset;

    public float shakeAmount;
    public float shakeDuration, shakeTimer;

    void Start()
    {
        newPosition.z = -10;
        shakeTimer = shakeDuration;
    }

    void Update()
    {
        if (shakeTimer < shakeDuration)
        {
            temporaryXOffset = Random.Range(-1, 2) * shakeAmount;
            temporaryYOffset = Random.Range(-1, 2) * shakeAmount;

            shakeTimer += Time.deltaTime;
        }
        else
        {
            temporaryXOffset = 0;
            temporaryYOffset = 0;
        }

        newPosition.x = playerTransform.position.x + temporaryXOffset;
        newPosition.y = playerTransform.position.y + temporaryYOffset;

        transform.position = newPosition;
    }
}
