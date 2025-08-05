using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject playerPrefab1;


    void Awake()
    {
        if (Instance == null) Instance = this;

        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
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
