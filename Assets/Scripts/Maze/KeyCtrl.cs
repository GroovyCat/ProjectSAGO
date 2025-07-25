using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyCtrl : MonoBehaviour
{
    public TMP_Text codeText;
    private bool isPressed = false;
    public float inputCooldown = 0.2f; // 쿨다운 시간 (초)
    public Animator[] animators;
    public Animator[] animators2;


    private void OnCollisionEnter(Collision collision)
    {
        if (!isPressed && collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ResetPressAfterDelay());
            foreach (Animator animator in animators)
            {
                animator.ResetTrigger("CloseDoor");
                animator.SetTrigger("DoorTrigger");
            }
            foreach (Animator animator2 in animators2)
            {
                animator2.ResetTrigger("DoorTrigger");
                animator2.SetTrigger("CloseDoor");
            }
        }
    }

    IEnumerator ResetPressAfterDelay()
    {
        isPressed = true;
        yield return new WaitForSeconds(inputCooldown);
        isPressed = false;
    }
}
