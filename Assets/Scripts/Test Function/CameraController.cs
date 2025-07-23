using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform follow; // 캐릭터의 Transform
    [SerializeField] private Camera mainCamera; // 메인 카메라 
    [SerializeField] private float distance = 5f; // 카메라와 캐릭터 사이의 거리
    [SerializeField] private float xSpeed = 4; // 마우스 X 축 회전 속도
    [SerializeField] private float ySpeed = 2; // 마우스 Y 축 회전 속도

    [SerializeField] private float yMinLimit = -10f; // Y 축 회전 제한 최소값
    [SerializeField] private float yMaxLimit = 40f;  // Y 축 회전 제한 최대값

    [SerializeField] private float xOffset = 0f; // 카메라의 X축 오프셋
    [SerializeField] private float yOffset = 0.3f; // 카메라의 Y축 오프셋
    [SerializeField] private float zOffset = 0f; // 카메라의 Z축 오프셋
    [SerializeField] private float zoomSpeed = 10.0f; // 줌 인/아웃 속도

    private float x = 0f; // 현재 X 축 회전값
    private float y = 0f; // 현재 Y 축 회전값

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
