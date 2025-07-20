using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChapterSLUI : MonoBehaviour
{
    public Button[] chapterButtons;

    void Start()
    {
        int maxCleared = PlayerPrefs.GetInt("MaxChapterCleared", -1);

        for (int i = 0; i < chapterButtons.Length; i++)
        {
            int index = i; // ��ư Ŭ�� �̺�Ʈ�� ���� ����
            bool unlocked = i <= maxCleared + 1;

            chapterButtons[i].interactable = unlocked;
            chapterButtons[i].onClick.AddListener(() => GameManager.Instance.StartChapter(index));
        }
    }
}
