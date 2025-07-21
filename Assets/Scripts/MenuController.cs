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
        Debug.Log("새 게임!");
        // 처음 넘어가는 씬삽입
    }
    public void OnClickLoadGame()
    {
        Debug.Log("불러오기");
        // 저장된 게임씬 부터 시작
    }

    public void OpenSettings()
    {
        settingUI.SetActive(true);
        Time.timeScale = 0f; // 게임 일시정지
    }

    public void CloseSettings()
    {
        settingUI.SetActive(false);
        Time.timeScale = 1f; // 게임 재개
    }

    public void OnClickQuitGame()
    {
        QuitGame();
    }
    void QuitGame()
    {
        Debug.Log("게임 종료!");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
