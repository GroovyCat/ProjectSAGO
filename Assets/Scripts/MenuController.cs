using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class MenuController : MonoBehaviour
{
    public GameObject settingUI;
    public void OnClickNewGame()
    {
        Debug.Log("�� ����!");
        // ó�� �Ѿ�� ������
    }
    public void OnClickLoadGame()
    {
        Debug.Log("�ҷ�����");
        // ����� ���Ӿ� ���� ����
    }

    public void OpenSettings()
    {
        settingUI.SetActive(true);
        Time.timeScale = 0f; // ���� �Ͻ�����
    }

    public void CloseSettings()
    {
        settingUI.SetActive(false);
        Time.timeScale = 1f; // ���� �簳
    }

    public void OnClickQuitGame()
    {
        QuitGame();
    }
    void QuitGame()
    {
        Debug.Log("���� ����!");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
