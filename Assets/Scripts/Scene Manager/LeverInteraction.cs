using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteraction : MonoBehaviour
{
    public Animator leverAnimator;        // ���� �ִϸ�����
    public GameObject door;               // ���� ��
    public Vector3 doorOpenOffset = new Vector3(0, 0, 3f); // �� ���� ����� �Ÿ�
    public float doorOpenSpeed = 2f;

    private bool playerInRange = false;
    private bool isDoorOpening = false;
    private Vector3 doorTargetPos;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E Ű ���� - ���� �۵� ����");
            leverAnimator.SetTrigger("Play"); // ���� �ִϸ��̼� ���
        }

        // �� ���� ����
        if (isDoorOpening)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, doorTargetPos, doorOpenSpeed * Time.deltaTime);

            if (Vector3.Distance(door.transform.position, doorTargetPos) < 0.01f)
            {
                isDoorOpening = false;
            }
        }
    }

    // �ִϸ��̼� �̺�Ʈ�� ȣ��� �Լ�
    public void OpenDoor()
    {
        Debug.Log("OpenDoor() ȣ��� - �� ���� ����");

        doorTargetPos = door.transform.position + doorOpenOffset;
        isDoorOpening = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) playerInRange = false;
    }
}

