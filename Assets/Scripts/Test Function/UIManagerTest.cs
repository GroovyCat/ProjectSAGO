using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerTest : MonoBehaviour
{
    public static UIManagerTest instance;

    public GameObject Background;
    public TMP_Text CanvasText;
    public Image interactiveImage;



    private void Awake()
    {
        instance = this;

        Background.SetActive(false);
        CanvasText.text = "";
    }

    public void ShowCanvasText(string str) //ĵ������ ������ ǥ���մϴ�
    {
        Background.SetActive(true); //��� ������ �մϴ�

        if (str == "Chair")
        {
            CanvasText.text = "�����Դϴ�";
        }
        else if (str == "Book")
        {
            interactiveImage.gameObject.SetActive(true);
        }
        else if (str == "Interactive")
        {
            CanvasText.text = "��ȣ�ۿ� ���� ������Ʈ �Դϴ�.";
        }
        else
        {
            CanvasText.text = "�±װ� �������� �ʾҽ��ϴ�.";
        }

        Invoke("DisableBackground", 1.0f); //1�� �ڿ� ��� ������ ���ϴ�
    }

    void DisableBackground()
    {
        Background.SetActive(false);
    }
}