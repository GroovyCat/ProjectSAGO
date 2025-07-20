using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterManager : MonoBehaviour
{
    public static ChapterManager Instance;

    [Header("챕터별 스폰 위치")]
    public Transform[] spawnPoints;

    [Header("플레이어 프리팹")]
    public GameObject playerPrefab;

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
        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        if (spawnPoints.Length <= CurrentChapter)
        {
            Debug.LogWarning("해당 챕터의 스폰 위치가 없습니다.");
            return;
        }

        GameObject existing = GameObject.FindGameObjectWithTag("Player");
        if (existing != null)
            Destroy(existing);

        Instantiate(playerPrefab, spawnPoints[CurrentChapter].position, Quaternion.identity);
    }

    public void CompleteChapter()
    {
        int maxClear = PlayerPrefs.GetInt("MaxChapterCleared", -1);
        if (CurrentChapter > maxClear)
        {
            PlayerPrefs.SetInt("MaxChapterCleared", CurrentChapter);
            Debug.Log($"챕터 {CurrentChapter} 클리어됨.");
        }
    }
}
