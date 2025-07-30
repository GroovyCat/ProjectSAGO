using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject playerPrefab1;

    public TutorialManager tutorialManagerFromPlayer;

    void Awake()
    {
        if (Instance == null) Instance = this;

        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    public void RegisterTutorialManager(TutorialManager tm)
    {
        tutorialManagerFromPlayer = tm;
    }

    public void TryStartTutorial()
    {
        if (tutorialManagerFromPlayer != null)
        {
            tutorialManagerFromPlayer.StartTutorial();
        }
        else
        {
            Debug.LogWarning("Ʃ�丮�� �Ŵ����� ������� �ʾҽ��ϴ�.");
        }
    }

    public void OpenSettings()
    {
        if (tutorialManagerFromPlayer != null)
        {
            tutorialManagerFromPlayer.settingsPanel.SetActive(true);
        }
    }

    public void SpawnPlayerAt(Transform spawnPoint)
    {
        GameObject existing = GameObject.FindGameObjectWithTag("Player");
        if (existing != null)
        {
            Debug.LogWarning("[GameManager] ���� �÷��̾� ���ŵ�");
            Destroy(existing);
        }
        Instantiate(playerPrefab1, spawnPoint.position, spawnPoint.rotation);
    }
    public void StartChapter(int chapterIndex)
    {
        ChapterManager.Instance?.LoadChapter(chapterIndex);
    }

    public void CompleteChapter()
    {
        ChapterManager.Instance?.CompleteChapter();
    }
  
}
