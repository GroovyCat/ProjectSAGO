using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampMove : MonoBehaviour
{
    public float swingAmplitude;  // 흔들림 각도
    public float swingSpeed;      // 흔들림 속도
    public float moveSpeed;       // 옆으로 움직이는 속도

    private float initialX;
    private Quaternion initialRotation;

    void Start()
    {
        initialX = transform.position.x;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // 진자처럼 흔들림 (Z축을 기준으로 회전)
        float angle = swingAmplitude * Mathf.Sin(Time.time * swingSpeed);
        transform.rotation = initialRotation * Quaternion.Euler(angle, 0f, 0f);

        // 옆으로 천천히 이동
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }
}