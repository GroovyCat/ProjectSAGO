using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCtrl : MonoBehaviour
{
    // 캐릭터 컨트롤러 컴포넌트 (중력,이동,충돌 감지용)
    public CharacterController player;
    // 캐릭터 이동 속도
    public float speed = 2.0f;
    // 중력 적용 값
    public float gravity = 30.0f;
    // 실제 이동방향을 저장하는 벡터
    Vector3 moveDirection = Vector3.zero;
    // 캐릭터 몸체 트랜스폼 (캐릭터 방향 전환시 사용)
    [SerializeField]
    private Transform characterBody;
    // 카메라 암(마우스 이동에 따라 회전하는 카메라부모 오브젝트)
    [SerializeField]
    private Transform cameraArm;

    Animator animator;
    // 회전 보간용 변수
    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    void Start()
    {
        //player.GetComponent<CharacterController>(); 필요없는 코드

        // 자식 오브젝트에서 animator찾기
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // 마우스 이동으로 카메라 회전
        LookAround();

        // 캐릭터가 지면에 닿았는가 리턴하는 함수를 적용

        if (player.isGrounded)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 inputDir = new Vector3(x, 0, z);

            if (inputDir.sqrMagnitude > 0.01f)
            {
                // 카메라 기준 방향으로 입력 방향 변환
                Vector3 camForward = cameraArm.forward;
                camForward.y = 0;
                camForward.Normalize();

                Vector3 camRight = cameraArm.right;
                camRight.y = 0;
                camRight.Normalize();

                Vector3 moveDir = camForward * z + camRight * x;
                moveDir.Normalize();

                // 부드러운 회전 처리 (SmoothDampAngle)
                float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(characterBody.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                characterBody.rotation = Quaternion.Euler(0, angle, 0);

                // 최종 이동 벡터 계산
                moveDirection = moveDir * speed;

                animator.SetBool("isMove", true);
            }
            else
            {
                moveDirection = Vector3.zero;
                animator.SetBool("isMove", false);
            }


        }

        // 중력 적용 (y방향으로 중력만큼 속도를 감소)
        moveDirection.y -= gravity * Time.deltaTime;
        // 캐릭터 이동 처리 (이동 벡터를 시간과 함께 곱해서 프레임 보정)
        player.Move(moveDirection * Time.deltaTime);

    }

    // 마우스 움직임에 따라 카메라 회전 처리
    private void LookAround()
    {
        // 마우스 이동 입력값 받기
        Vector2 mouseDelta = new Vector2
            (Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // 현재 카메라 회전 각도 가져오기
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        // 수직 회전각 계산 (마우스 y축 이동을 위아래로 반영)
        float x = camAngle.x - mouseDelta.y;

        // 카메라 위 아래 회전 제한 
        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        // 카메라 암 회전 적용 (마우스 x축은 좌우 회전)
        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }
}
