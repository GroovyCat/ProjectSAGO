using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastCtrl : MonoBehaviour
{
    public float raycastDistance = 3f; // 인식 거리
    public Camera playerCamera;       // 플레이어 카메라 참조

    private GameObject backupObject = null;
    private bool raycastOn;

    RaycastHit hit;
    Ray ray;

    void Update()
    {
        raycastOn = false;
        // 카메라 기준 origin, direction
        Vector3 origin = playerCamera.transform.position;
        Vector3 direction = playerCamera.transform.forward;

        ray = new Ray(origin, direction);

        // 디버그 라인 표시
        Debug.DrawLine(origin, origin + direction * raycastDistance, Color.red);


        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("Interactive"))
            {
                if (hitObject != backupObject)
                {
                    DeactivatePrevious(); // 이전 감지 해제

                    backupObject = hitObject;

                    Canvas canvas = hitObject.GetComponentInChildren<Canvas>(true);
                    if (canvas != null)
                    {
                        canvas.gameObject.SetActive(true);
                    }

                    Debug.Log($"[RayCastCtrl] 감지된 오브젝트: {hitObject.name}");
                }

                return;
            }
        }

        // 감지된 Interactive가 없으면 이전 오브젝트 비활성화
        DeactivatePrevious();
    }

    private void DeactivatePrevious()
    {
        if (backupObject != null)
        {
            Canvas canvas = backupObject.GetComponentInChildren<Canvas>(true);
            if (canvas != null)
            {
                canvas.gameObject.SetActive(false);
            }

            backupObject = null;
        }
    }
}