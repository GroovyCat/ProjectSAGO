using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class TpsChaCtrl : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform characterBody;  // 캐릭터 모델
    [SerializeField] private Transform cameraArm;       // 카메라 트랜스폼

    private CharacterController controller;
    private Animator animator;

    [Header("Movement Settings")]
    public float speed = 5.0f;
    public float gravity = 30.0f;
    public float jumpPower = 10.0f;

    private Vector3 velocity;

    public float turnSmoothTime = 0.1f;  // 회전 부드럽기 조절
    private float turnSmoothVelocity;

    // 더블점프 관련 변수
    private int currentJumpCount = 0;
    public int maxJumpCount = 2;

    // 구르기 관련 변수
    [Header("Roll Settings")]
    public float rollSpeed = 8f;
    public float rollDuration = 0.6f;

    private bool isRolling = false;
    private float rollTimer = 0f;
    private Vector3 rollDirection;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = characterBody.GetComponent<Animator>();
    }

    void Update()
    {
        LookAround();
        if (!isRolling)
        {
            Move(); // 이동/점프는 구르기 중에는 막음
        }

        HandleRoll(); // 구르기는 항상 체크

        if (Input.GetKeyDown(KeyCode.E)) //키보드 E를 눌렀을 때
        {
            animator.SetTrigger("isRoot");
            Debug.Log("감지됨");
        }
    }

    private void Move()
    {
        // 입력값 받아오기
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");
        Vector2 moveInput = new Vector2(inputX, inputZ);
        bool isMoving = moveInput.magnitude > 0;

        // 애니메이션 업데이트
        animator.SetBool("isMove", isMoving);

        // 카메라 기준 방향 벡터 계산
        Vector3 forward = new Vector3(cameraArm.forward.x, 0, cameraArm.forward.z).normalized;
        Vector3 right = new Vector3(cameraArm.right.x, 0, cameraArm.right.z).normalized;
        Vector3 moveDir = (forward * inputZ + right * inputX).normalized;

        // ✅ 부드러운 회전 처리 (Lerp 대신 SmoothDampAngle 사용)
        if (isMoving)
        {
            float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
            float currentAngle = Mathf.SmoothDampAngle(characterBody.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            characterBody.rotation = Quaternion.Euler(0, currentAngle, 0);
        }


        if (controller.isGrounded)
        {
            currentJumpCount = 0;
            velocity.y = -2f; // 바닥에 밀착

            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = jumpPower;
                currentJumpCount++;
                if (currentJumpCount == 1)
                {
                    animator.SetTrigger("isJump2");
                }
                else
                animator.SetTrigger("isJump");
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && currentJumpCount < maxJumpCount)
            {
                velocity.y = jumpPower;
                currentJumpCount++;
                animator.SetTrigger("isJump");
            }

            // 중력 적용
            velocity.y -= gravity * Time.deltaTime;
        }

            Vector3 finalMove = moveDir * speed;
        finalMove.y = velocity.y;

        controller.Move(finalMove * Time.deltaTime);
    }

    private void LookAround()
    {
        Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        Vector3 camAngle = cameraArm.rotation.eulerAngles;

        float x = camAngle.x - mouseDelta.y;

        // 수직 회전 제한
        if (x < 180f)
            x = Mathf.Clamp(x, -1f, 70f);
        else
            x = Mathf.Clamp(x, 335f, 361f);

        float y = camAngle.y + mouseDelta.x;

        cameraArm.rotation = Quaternion.Euler(x, y, 0f);
    }

    private void HandleRoll()
    {
        if (isRolling)
        {
            rollTimer -= Time.deltaTime;
            if (rollTimer <= 0f)
            {
                isRolling = false;
            }

            // 구르기 방향으로 이동 유지
            Vector3 rollMove = rollDirection * rollSpeed;
            rollMove.y = velocity.y; // 중력 유지
            controller.Move(rollMove * Time.deltaTime);
        }
        else
        {
            // 구르기 입력 (예: LeftShift)
            if (controller.isGrounded && Input.GetKeyDown(KeyCode.LeftShift))
            {
                Vector3 inputDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                if (inputDir.sqrMagnitude > 0.1f)
                {
                    Vector3 camForward = new Vector3(cameraArm.forward.x, 0, cameraArm.forward.z).normalized;
                    Vector3 camRight = new Vector3(cameraArm.right.x, 0, cameraArm.right.z).normalized;
                    rollDirection = (camForward * inputDir.z + camRight * inputDir.x).normalized;
                }
                else
                {
                    rollDirection = characterBody.forward; // 정지 중이면 바라보는 방향으로 구르기
                }

                isRolling = true;
                rollTimer = rollDuration;

                animator.SetTrigger("isRoll");
            }
        }
    }
}
