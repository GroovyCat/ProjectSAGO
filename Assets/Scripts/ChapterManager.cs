using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ChapterManager : MonoBehaviour
{
    public static ChapterManager Instance;

    [Header("Ã©ÅÍº° ½ºÆù À§Ä¡")]
    public Transform[] spawnPoints;

    public int CurrentChapter { get; private set; }

    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadChapter(int chapterIndex)
    {
        CurrentChapter = chapterIndex;
        PlayerPrefs.SetInt("CurrentChapter", chapterIndex);

        string sceneName = $"Chapter{chapterIndex}";
        SceneManager.LoadScene(sceneName);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(DelayedSpawn());
    }

    IEnumerator DelayedSpawn()
    {
        // GameManager.Instance°¡ nullÀÌ¸é Àá±ñ ´ë±â
        while (GameManager.Instance == null)
        {
            yield return null;
        }

        if (spawnPoints == null || spawnPoints.Length <= CurrentChapter || spawnPoints[CurrentChapter] == null)
        {
            yield break;
        }

        GameManager.Instance.SpawnPlayerAt(spawnPoints[CurrentChapter]);
    }
   

    public void CompleteChapter()
    {
        int maxClear = PlayerPrefs.GetInt("MaxChapterCleared", -1);
        if (CurrentChapter > maxClear)
        {
            PlayerPrefs.SetInt("MaxChapterCleared", CurrentChapter);
            Debug.Log($"Ã©ÅÍ {CurrentChapter} Å¬¸®¾îµÊ.");
        }
    }

}
