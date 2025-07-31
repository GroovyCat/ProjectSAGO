using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastCtrl : MonoBehaviour
{
    public float raycastDistance = 3f; // �ν� �Ÿ�
    public Camera playerCamera;       // �÷��̾� ī�޶� ����

    private GameObject backupObject = null;
    private bool raycastOn;

    RaycastHit hit;
    Ray ray;

    void Update()
    {
        raycastOn = false;
        // ī�޶� ���� origin, direction
        Vector3 origin = playerCamera.transform.position;
        Vector3 direction = playerCamera.transform.forward;

        ray = new Ray(origin, direction);

        // ����� ���� ǥ��
        Debug.DrawLine(origin, origin + direction * raycastDistance, Color.red);


        if (Physics.Raycast(ray, out RaycastHit hit, raycastDistance))
        {
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject.CompareTag("Interactive"))
            {
                if (hitObject != backupObject)
                {
                    DeactivatePrevious(); // ���� ���� ����

                    backupObject = hitObject;

                    Canvas canvas = hitObject.GetComponentInChildren<Canvas>(true);
                    if (canvas != null)
                    {
                        canvas.gameObject.SetActive(true);
                    }

                    Debug.Log($"[RayCastCtrl] ������ ������Ʈ: {hitObject.name}");
                }

                return;
            }
        }

        // ������ Interactive�� ������ ���� ������Ʈ ��Ȱ��ȭ
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