using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuUI : MonoBehaviour
{
  
    public void OnClickNewGame()
    {
        Debug.Log("�� ����!");
    }
    public void OnClickLoadGame()
    {
        Debug.Log("�ҷ�����");
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
