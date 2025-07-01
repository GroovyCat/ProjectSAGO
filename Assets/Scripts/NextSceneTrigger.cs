using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextSceneTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // 플레이어 오브젝트에 플레이어 태그 추가
        {
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; // 빌드에 씬 추가 해주세요
            SceneManager.LoadScene(currentSceneIndex + 1); //다음씬 불러오기(순서는 회의날짜에 빌드된거 보고 정하기)
        }
    }
}
