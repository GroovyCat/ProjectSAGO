using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastCtrl : MonoBehaviour
{
    public float raycastDistance = 3f; // 인식 거리
    public Camera playerCamera;       // 플레이어 카메라 참조

    RaycastHit hit;
    Ray ray;

    void Update()
    {
        // 카메라 기준 origin, direction
        Vector3 origin = playerCamera.transform.position;
        Vector3 direction = playerCamera.transform.forward;

        ray = new Ray(origin, direction);

        // 디버그 라인 표시
        Debug.DrawLine(origin, origin + direction * raycastDistance, Color.red);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                GameObject hitObject = hit.collider.gameObject;

                if (hitObject != null)
                {
                    Debug.Log($"[RayCastCtrl] 감지된 오브젝트: {hitObject.name}");
                }
            }
        }
    }
}
