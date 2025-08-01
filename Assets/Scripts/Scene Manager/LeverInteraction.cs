using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverInteraction : MonoBehaviour
{
    public Animator leverAnimator;        // 레버 애니메이터
    public GameObject door;               // 열릴 문
    public Vector3 doorOpenOffset = new Vector3(0, 0, 3f); // 문 열릴 방향과 거리
    public float doorOpenSpeed = 2f;

    private bool playerInRange = false;
    private bool isDoorOpening = false;
    private Vector3 doorTargetPos;

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("E 키 눌림 - 레버 작동 시작");
            leverAnimator.SetTrigger("Play"); // 레버 애니메이션 재생
        }

        // 문 열기 동작
        if (isDoorOpening)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, doorTargetPos, doorOpenSpeed * Time.deltaTime);

            if (Vector3.Distance(door.transform.position, doorTargetPos) < 0.01f)
            {
                isDoorOpening = false;
            }
        }
    }

    // 애니메이션 이벤트로 호출될 함수
    public void OpenDoor()
    {
        Debug.Log("OpenDoor() 호출됨 - 문 열기 시작");

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

