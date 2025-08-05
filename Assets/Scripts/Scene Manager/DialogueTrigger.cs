using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;

    public void ShowText(string message)
    {
        dialogueText.text = message;
        dialogueText.gameObject.SetActive(true);
    }

    public void HideText()
    {
        dialogueText.gameObject.SetActive(false);
    }
}
