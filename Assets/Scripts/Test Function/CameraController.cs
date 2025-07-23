using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform follow; // ĳ������ Transform
    [SerializeField] private Camera mainCamera; // ���� ī�޶� 
    [SerializeField] private float distance = 5f; // ī�޶�� ĳ���� ������ �Ÿ�
    [SerializeField] private float xSpeed = 4; // ���콺 X �� ȸ�� �ӵ�
    [SerializeField] private float ySpeed = 2; // ���콺 Y �� ȸ�� �ӵ�

    [SerializeField] private float yMinLimit = -10f; // Y �� ȸ�� ���� �ּҰ�
    [SerializeField] private float yMaxLimit = 40f;  // Y �� ȸ�� ���� �ִ밪

    [SerializeField] private float xOffset = 0f; // ī�޶��� X�� ������
    [SerializeField] private float yOffset = 0.3f; // ī�޶��� Y�� ������
    [SerializeField] private float zOffset = 0f; // ī�޶��� Z�� ������
    [SerializeField] private float zoomSpeed = 10.0f; // �� ��/�ƿ� �ӵ�

    private float x = 0f; // ���� X �� ȸ����
    private float y = 0f; // ���� Y �� ȸ����

    void Start()
    {
        var angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    void Update()
    {
        Zoom();
    }

    void LateUpdate()
    {
        x += Input.GetAxis("Mouse X") * xSpeed;
        y -= Input.GetAxis("Mouse Y") * ySpeed;

        y = Mathf.Clamp(y, yMinLimit, yMaxLimit);

        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 position = follow.position - (rotation * Vector3.forward * distance);
        position += new Vector3(xOffset, yOffset, zOffset);

        transform.rotation = rotation;
        transform.position = position;
    }


    private void Zoom()
    {
        float distance = Input.GetAxis("Mouse ScrollWheel") * -1 * zoomSpeed;
        if (distance != 0)
        {
            mainCamera.fieldOfView += distance;
        }
    }
}
