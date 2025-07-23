using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCtrl : MonoBehaviour
{
    // ĳ���� ��Ʈ�ѷ� ������Ʈ (�߷�,�̵�,�浹 ������)
    public CharacterController player;
    // ĳ���� �̵� �ӵ�
    public float speed = 2.0f;
    // �߷� ���� ��
    public float gravity = 30.0f;
    // ���� �̵������� �����ϴ� ����
    Vector3 moveDirection = Vector3.zero;
    // ĳ���� ��ü Ʈ������ (ĳ���� ���� ��ȯ�� ���)
    [SerializeField]
    private Transform characterBody;
    // ī�޶� ��(���콺 �̵��� ���� ȸ���ϴ� ī�޶�θ� ������Ʈ)
    [SerializeField]
    private Transform cameraArm;

    Animator animator;
    // ȸ�� ������ ����
    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    void Start()
    {
        //player.GetComponent<CharacterController>(); �ʿ���� �ڵ�

        // �ڽ� ������Ʈ���� animatorã��
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        // ���콺 �̵����� ī�޶� ȸ��
        LookAround();

        // ĳ���Ͱ� ���鿡 ��Ҵ°� �����ϴ� �Լ��� ����

        if (player.isGrounded)
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 inputDir = new Vector3(x, 0, z);

            if (inputDir.sqrMagnitude > 0.01f)
            {
                // ī�޶� ���� �������� �Է� ���� ��ȯ
                Vector3 camForward = cameraArm.forward;
                camForward.y = 0;
                camForward.Normalize();

                Vector3 camRight = cameraArm.right;
                camRight.y = 0;
                camRight.Normalize();

                Vector3 moveDir = camForward * z + camRight * x;
                moveDir.Normalize();

                // �ε巯�� ȸ�� ó�� (SmoothDampAngle)
                float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
                float angle = Mathf.SmoothDampAngle(characterBody.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                characterBody.rotation = Quaternion.Euler(0, angle, 0);

                // ���� �̵� ���� ���
                moveDirection = moveDir * speed;

                animator.SetBool("isMove", true);
            }
            else
            {
                moveDirection = Vector3.zero;
                animator.SetBool("isMove", false);
            }


        }

        // �߷� ���� (y�������� �߷¸�ŭ �ӵ��� ����)
        moveDirection.y -= gravity * Time.deltaTime;
        // ĳ���� �̵� ó�� (�̵� ���͸� �ð��� �Բ� ���ؼ� ������ ����)
        player.Move(moveDirection * Time.deltaTime);

    }

    // ���콺 �����ӿ� ���� ī�޶� ȸ�� ó��
    private void LookAround()
    {
        // ���콺 �̵� �Է°� �ޱ�
        Vector2 mouseDelta = new Vector2
            (Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        // ���� ī�޶� ȸ�� ���� ��������
        Vector3 camAngle = cameraArm.rotation.eulerAngles;
        // ���� ȸ���� ��� (���콺 y�� �̵��� ���Ʒ��� �ݿ�)
        float x = camAngle.x - mouseDelta.y;

        // ī�޶� �� �Ʒ� ȸ�� ���� 
        if (x < 180f)
        {
            x = Mathf.Clamp(x, -1f, 70f);
        }
        else
        {
            x = Mathf.Clamp(x, 335f, 361f);
        }

        // ī�޶� �� ȸ�� ���� (���콺 x���� �¿� ȸ��)
        cameraArm.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }
}
