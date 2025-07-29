using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastCtrl : MonoBehaviour
{
    public float raycastDistance = 3f; // �ν� �Ÿ�
    public Camera playerCamera;       // �÷��̾� ī�޶� ����

    RaycastHit hit;
    Ray ray;

    void Update()
    {
        // ī�޶� ���� origin, direction
        Vector3 origin = playerCamera.transform.position;
        Vector3 direction = playerCamera.transform.forward;

        ray = new Ray(origin, direction);

        // ����� ���� ǥ��
        Debug.DrawLine(origin, origin + direction * raycastDistance, Color.red);

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (Physics.Raycast(ray, out hit, raycastDistance))
            {
                GameObject hitObject = hit.collider.gameObject;

                if (hitObject != null)
                {
                    Debug.Log($"[RayCastCtrl] ������ ������Ʈ: {hitObject.name}");
                }
            }
        }
    }
}
