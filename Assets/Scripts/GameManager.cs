using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public ChapterManager chapterManager;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (chapterManager == null)
        {
            chapterManager = GetComponentInChildren<ChapterManager>();
        }
    }

    public void StartChapter(int chapterIndex)
    {
        chapterManager.LoadChapter(chapterIndex);
    }

    public void CompleteChapter()
    {
        chapterManager.CompleteChapter();
    }

}
