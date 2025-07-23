using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightMove : MonoBehaviour
{

    public Vector3 moveDirection = Vector3.right; // �̵� ���� (��: Vector3.up, Vector3.forward)
    public float distance; // �ִ� �̵� �Ÿ�
    public float speed;    // �̵� �ӵ�

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
