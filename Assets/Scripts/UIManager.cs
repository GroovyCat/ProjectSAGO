using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject Background;
    public TMP_Text CanvasText;
    public Image interactiveImage;



    private void Awake()
    {
        instance = this;

        Background.SetActive(false);
        CanvasText.text = "";
    }

    public void ShowCanvasText(string str) //캔버스에 정보를 표시합니다
    {
        Background.SetActive(true); //흰색 바탕을 켭니다

        if (str == "Chair")
        {
            CanvasText.text = "의자입니다";
        }
        else if (str == "Book")
        {
            interactiveImage.gameObject.SetActive(true);
        }
        else if (str == "Interactive")
        {
            CanvasText.text = "상호작용 가능 오브젝트 입니다.";
        }
        else
        {
            CanvasText.text = "태그가 설정되지 않았습니다.";
        }

        Invoke("DisableBackground", 1.0f); //1초 뒤에 흰색 바탕을 끕니다
    }

    void DisableBackground()
    {
        Background.SetActive(false);
    }
}