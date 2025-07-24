using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomLoadingMessage : MonoBehaviour
{
    public TextMeshProUGUI textBase;  // 회색 텍스트
    public TextMeshProUGUI textFill;  // 채워지는 컬러 텍스트

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
