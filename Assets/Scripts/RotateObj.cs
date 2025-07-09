using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObj : MonoBehaviour
{
  
    public Transform centerPoint;             // 회전 중심점
    public float rotationSpeed = 30f;         // 회전 속도
    public float rotateDuration = 3f;         // 회전 시간 (초)
    public float pauseDuration = 2f;          // 정렬 후 멈추는 시간 (초)

    private float timer = 0f;
    private bool isRotating = true;
    private Quaternion targetRotation;

    void Start()
    {
        SetNextTargetRotation();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (isRotating)
        {
            // 회전 중일 때
            transform.RotateAround(centerPoint.position, Vector3.up, rotationSpeed * Time.deltaTime);

            if (timer >= rotateDuration)
            {
                isRotating = false;
                timer = 0f;

                // 가장 가까운 90도 방향으로 정렬
                float yRot = Mathf.Round(transform.eulerAngles.y / 90f) * 90f;
                targetRotation = Quaternion.Euler(0f, yRot, 0f);
            }
        }
        else
        {
            // 정렬 중 (부드럽게 회전 정렬)
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);

            if (timer >= pauseDuration)
            {
                isRotating = true;
                timer = 0f;
            }
        }
    }

    void SetNextTargetRotation()
    {
        float yRot = Mathf.Round(transform.eulerAngles.y / 90f) * 90f;
        targetRotation = Quaternion.Euler(0f, yRot, 0f);
    }
}

