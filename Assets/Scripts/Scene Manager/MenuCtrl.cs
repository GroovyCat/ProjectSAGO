using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class MenuCtrl : MonoBehaviour
{
    public GameObject settingUI;
    public void OnClickNewGame()
    {
        Debug.Log("새 게임!");
        PlayerPrefs.SetString("NextScene", "1.Third Floor Scene"); // 새게임용 씬



        SceneManager.LoadScene("LoadingScene");
    }
    public void OnClickLoadChapterScene(string sceneName, int chapterIndex)
    {
        Debug.Log($"불러오기: {sceneName}, 챕터: {chapterIndex}");
        PlayerPrefs.SetString("NextScene", sceneName);
        PlayerPrefs.SetInt("SelectedChapter", chapterIndex);
        SceneManager.LoadScene("LoadingScene");
    }
    public void OnClickLoadChapter1()
    {
        OnClickLoadChapterScene("1.Third Floor Scene", 1);
    }

    public void OnClickLoadChapter2()
    {
        OnClickLoadChapterScene("2.Second Floor 1Scene", 2);
    }
    public void OnClickLoadChapter3()
    {
        OnClickLoadChapterScene("3.Second Floor 2 Scene", 3);
    }


    public void OpenSettings()
    {
        settingUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseSettings()
    {
        settingUI.SetActive(false);
        Time.timeScale = 1f; 
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
