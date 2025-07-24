using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomLoadingMessage : MonoBehaviour
{
    public TextMeshProUGUI textBase;  // ȸ�� �ؽ�Ʈ
    public TextMeshProUGUI textFill;  // ä������ �÷� �ؽ�Ʈ

    [TextArea(2, 5)]
    public string[] loadingMessages; 

    void Start()
    {
        if (loadingMessages.Length == 0) return;

        string selected = loadingMessages[Random.Range(0, loadingMessages.Length)];

        textBase.text = selected;
        textFill.text = selected;
    }
}
