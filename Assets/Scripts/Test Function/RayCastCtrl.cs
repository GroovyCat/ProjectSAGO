using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayCastCtrl : MonoBehaviour
{
    public float raycastDistance = 3f; //인식할 수 있는 범위
    public Transform characterBody;

    RaycastHit hit;
    Ray ray;

    void Update()
    {
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * raycastDistance, Color.red); //씬에서 내가 보고있는 방향을 표시

        Vector3 origin = characterBody.position + Vector3.up * 1f; // 조금 위에서 쏘면 정확도 향상
        Vector3 direction = characterBody.forward;

        ray = new Ray(transform.position, direction); //보고있는 방향으로 살펴보기

        //ray = Camera.main.ScreenPointToRay(Input.mousePosition); //마우스로 살펴보기

        if (Input.GetKeyDown(KeyCode.E)) //키보드 E를 눌렀을 때
        {
            if (Physics.Raycast(ray, out hit, raycastDistance)) //인식할 수 있는 범위 안에서 물체 확인
            {
                GameObject hitObject = hit.collider.gameObject; //주변 물체의 정보를 가져옵니다.

                if (hitObject != null) //물체가 있을 경우
                {
                    UIManagerTest.instance.ShowCanvasText(hitObject.tag);
                }
            }
        }
    }
}


// 물건 감지 
// 1. 콜라이더 방식
// 2. 코드 사용하여 transform.forward, 현재 위치 - 감지할 오브젝트 위치 //정면과 오브젝트위치 각도 계산등 코드로 계산하여 범위감지
