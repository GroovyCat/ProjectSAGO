using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // 컴포넌트를 캐시 처리할 변수
    private Transform tr;
    // 이동 속력 변수
    public float moveSpeed;     // 10
    // 회전 속도 변수
    public float turnSpeed;     // 80


    private void Start()
    {
        tr = GetComponent<Transform>();
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float r = Input.GetAxis("Mouse X");

        // 전후좌우 이동 방향 벡터 계산
        Vector3 moveDir = (Vector3.forward * v) + (Vector3.right * h);

        // Translate(이동 방향 * 속력 * Time.deltaTime)
        tr.Translate(moveDir * moveSpeed * Time.deltaTime);

        // Vector3.up 축을 기준으로 turnSpeed만큼의 속도로 회전
        tr.Rotate(Vector3.up * turnSpeed * Time.deltaTime * r);
    }
}
