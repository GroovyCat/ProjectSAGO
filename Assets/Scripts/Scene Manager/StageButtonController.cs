using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageButtonController : MonoBehaviour
{
    [System.Serializable]
    public class StageButtonInfo
    {
        public Button button;
        public string sceneName;   // 로드할 씬 이름
        public string displayName; // 표시용 한글 이름
        public int stageNumber;    // 예: 1, 2, 3...
    }

    public StageButtonInfo[] stageButtons;

    void Start()
    {
        foreach (var stage in stageButtons)
        {
            bool isUnlocked = false;

            if (stage.stageNumber == 1)
            {
                isUnlocked = true; // Stage1은 항상 열림
            }
            else
            {
                int prevStage = stage.stageNumber - 1;
                isUnlocked = PlayerPrefs.GetInt($"Stage{prevStage}_Clear", 0) == 1;
            }

            stage.button.interactable = isUnlocked;

        

            // 버튼 클릭 시 씬 로드 연결
            string sceneNameCopy = stage.sceneName; // 클로저 캡처 방지
            stage.button.onClick.AddListener(() =>
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneNameCopy);
            });
        }
    }
}
