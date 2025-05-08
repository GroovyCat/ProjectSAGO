using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 7.0f;

    float gravity = -20.0f; // 중력
    float yVelocity = 0.0f; // y축 속력
    float jumpPower = 5.0f; // 점프 force
    bool isJumping = false; // 점프 중복 체크

    private void Update()
    {
        var h = Input.GetAxis("Horizontal");
        var v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, 0, v);
        dir = dir.normalized;

        dir = Camera.main.transform.TransformDirection(dir); // 카메라가 바라본 방향
        transform.position += dir * moveSpeed; // 해당 방향으로 이동

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            yVelocity = jumpPower;
            isJumping = true;
        }
        yVelocity += gravity * Time.deltaTime; // 중력가속도 
        dir.y = yVelocity; // 해당 방향으로 중력 연산

        if (TryGetComponent(out CharacterController cc))
        {
            cc.Move(dir * moveSpeed * Time.deltaTime); // 이동
            if (isJumping && cc.collisionFlags == CollisionFlags.Below)
            {
                isJumping = false;
                yVelocity = 0.0f;
            }
        }
    }

}
