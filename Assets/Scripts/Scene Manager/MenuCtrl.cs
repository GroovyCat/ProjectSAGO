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
        PlayerPrefs.SetInt("StartWithTutorial", 1); // 튜토리얼 플래그
        PlayerPrefs.Save();
        SceneManager.LoadScene("1.Third Floor Scene");
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
        OnClickLoadChapterScene("2.Maze Scene", 2);
    }
    public void OnClickLoadChapter3()
    {
        OnClickLoadChapterScene("3.Second Floor Scene", 3);
    }
    public void OnClickLoadChapter4()
    {
        OnClickLoadChapterScene("4.Drone Scene", 4);
    }
    public void OnClickLoadChapter5()
    {
        OnClickLoadChapterScene("5.First Floor Boss Scene", 5);
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
