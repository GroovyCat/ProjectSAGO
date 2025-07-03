using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMove : MonoBehaviour
{

    public Vector3 moveDirection = Vector3.right; // 이동 방향 (예: Vector3.up, Vector3.forward)
    public float distance; // 최대 이동 거리
    public float speed;    // 이동 속도

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float offset = Mathf.PingPong(Time.time * speed, distance);
        transform.position = startPos + moveDirection.normalized * offset;
    }
}
