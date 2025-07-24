using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneCtrl : MonoBehaviour
{
    public Slider loadingBar;

    private string nextSceneName;

    private void Start()
    {
        nextSceneName = PlayerPrefs.GetString("NextScene", "1.Third Floor Scene"); // 기본값은 새 게임
        StartCoroutine(LoadScene());
    }

    IEnumerator LoadScene()
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextSceneName);
        op.allowSceneActivation = false;

        float timer = 0f;

        while (!op.isDone)
        {
            if (op.progress < 0.9f)
            {
                loadingBar.value = Mathf.Lerp(loadingBar.value, op.progress, Time.deltaTime * 1.5f);
            }
            else
            {
                loadingBar.value = Mathf.Lerp(loadingBar.value, 1f, Time.deltaTime * 1.5f);

                timer += Time.deltaTime;
                if (loadingBar.value >= 0.99f && timer > 1f)
                {
                    op.allowSceneActivation = true;
                }
            }

            yield return null;
        }
    }
}