using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookShelfMove : MonoBehaviour
{
  
    public Transform centerPoint;             // ȸ�� �߽���
    public float rotationSpeed = 30f;         // ȸ�� �ӵ�
    public float rotateDuration = 3f;         // ȸ�� �ð� (��)
    public float pauseDuration = 2f;          // ���� �� ���ߴ� �ð� (��)

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
            // ȸ�� ���� ��
            transform.RotateAround(centerPoint.position, Vector3.up, rotationSpeed * Time.deltaTime);

            if (timer >= rotateDuration)
            {
                isRotating = false;
                timer = 0f;

                // ���� ����� 90�� �������� ����
                float yRot = Mathf.Round(transform.eulerAngles.y / 90f) * 90f;
                targetRotation = Quaternion.Euler(0f, yRot, 0f);
            }
        }
        else
        {
            // ���� �� (�ε巴�� ȸ�� ����)
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

