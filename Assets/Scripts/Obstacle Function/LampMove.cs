using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampMove : MonoBehaviour
{
    public float swingAmplitude;  // ��鸲 ����
    public float swingSpeed;      // ��鸲 �ӵ�
    public float moveSpeed;       // ������ �����̴� �ӵ�

    private float initialX;
    private Quaternion initialRotation;

    void Start()
    {
        initialX = transform.position.x;
        initialRotation = transform.rotation;
    }

    void Update()
    {
        // ����ó�� ��鸲 (Z���� �������� ȸ��)
        float angle = swingAmplitude * Mathf.Sin(Time.time * swingSpeed);
        transform.rotation = initialRotation * Quaternion.Euler(angle, 0f, 0f);

        // ������ õõ�� �̵�
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }
}