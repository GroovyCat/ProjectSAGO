using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 7.0f;

    float gravity = -20.0f; // �߷�
    float yVelocity = 0.0f; // y�� �ӷ�
    float jumpPower = 5.0f; // ���� force
    bool isJumping = false; // ���� �ߺ� üũ

    private void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        dir = Camera.main.transform.TransformDirection(dir); // ī�޶� �ٶ� ����
        transform.position += dir * moveSpeed; // �ش� �������� �̵�

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            yVelocity = jumpPower;
            isJumping = true;
        }
        yVelocity += gravity * Time.deltaTime; // �߷°��ӵ� 
        dir.y = yVelocity; // �ش� �������� �߷� ����

        if (TryGetComponent(out CharacterController cc))
        {
            cc.Move(dir * moveSpeed * Time.deltaTime); // �̵�
            if (isJumping && cc.collisionFlags == CollisionFlags.Below)
            {
                isJumping = false;
                yVelocity = 0.0f;
            }
        }
    }

}
