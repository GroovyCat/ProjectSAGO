using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class FadeInManager : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1.5f;

    [Header("연출 대상")]
    public PlayableDirector timeline; // 씬에 있는 타임라인
    public GameObject player;         // 플레이어 오브젝트

    private void Start()
    {
        if (player != null) player.SetActive(false);
        if (timeline != null)
        {
            timeline.time = 0;
            timeline.Stop();
        }

        StartCoroutine(FadeInAndPlayTimeline());
    }

    IEnumerator FadeInAndPlayTimeline()
    {
        // 검은 화면에서 투명으로 서서히 전환
        float t = 0f;
        Color c = fadeImage.color;
        c.a = 1f;
        fadeImage.color = c;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            c.a = Mathf.Lerp(1f, 0f, t / fadeDuration);
            fadeImage.color = c;
            yield return null;
        }

        c.a = 0f;
        fadeImage.color = c;
        fadeImage.gameObject.SetActive(false);

        // 타임라인 재생
        if (timeline != null)
        {
            timeline.Play();
            yield return new WaitUntil(() => timeline.state != PlayState.Playing);
        }

        // 타임라인 종료 → 플레이어 활성화
        if (player != null) player.SetActive(true);
    }
}