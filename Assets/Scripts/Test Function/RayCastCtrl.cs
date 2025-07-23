using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastCtrl : MonoBehaviour
{
    public float raycastDistance = 3f; //�ν��� �� �ִ� ����
    public Transform characterBody;

    RaycastHit hit;
    Ray ray;

    void Update()
    {
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * raycastDistance, Color.red); //������ ���� �����ִ� ������ ǥ��

        Vector3 origin = characterBody.position + Vector3.up * 1f; // ���� ������ ��� ��Ȯ�� ���
        Vector3 direction = characterBody.forward;

        ray = new Ray(transform.position, direction); //�����ִ� �������� ���캸��

        //ray = Camera.main.ScreenPointToRay(Input.mousePosition); //���콺�� ���캸��

        if (Input.GetKeyDown(KeyCode.E)) //Ű���� E�� ������ ��
        {
            if (Physics.Raycast(ray, out hit, raycastDistance)) //�ν��� �� �ִ� ���� �ȿ��� ��ü Ȯ��
            {
                GameObject hitObject = hit.collider.gameObject; //�ֺ� ��ü�� ������ �����ɴϴ�.

                if (hitObject != null) //��ü�� ���� ���
                {
                    UIManagerTest.instance.ShowCanvasText(hitObject.tag);
                }
            }
        }
    }
}


// ���� ���� 
// 1. �ݶ��̴� ���
// 2. �ڵ� ����Ͽ� transform.forward, ���� ��ġ - ������ ������Ʈ ��ġ //����� ������Ʈ��ġ ���� ���� �ڵ�� ����Ͽ� ��������
