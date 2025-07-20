using UnityEngine;
using UnityEngine.SceneManagement;

public class ChapterTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.CompleteChapter();
        }
    }
}
