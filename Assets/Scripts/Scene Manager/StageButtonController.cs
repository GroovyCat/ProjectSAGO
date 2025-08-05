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
        public string sceneName;   // �ε��� �� �̸�
        public string displayName; // ǥ�ÿ� �ѱ� �̸�
        public int stageNumber;    // ��: 1, 2, 3...
    }

    public StageButtonInfo[] stageButtons;

    void Start()
    {
        foreach (var stage in stageButtons)
        {
            bool isUnlocked = false;

            if (stage.stageNumber == 1)
            {
                isUnlocked = true; // Stage1�� �׻� ����
            }
            else
            {
                int prevStage = stage.stageNumber - 1;
                isUnlocked = PlayerPrefs.GetInt($"Stage{prevStage}_Clear", 0) == 1;
            }

            stage.button.interactable = isUnlocked;

        

            // ��ư Ŭ�� �� �� �ε� ����
            string sceneNameCopy = stage.sceneName; // Ŭ���� ĸó ����
            stage.button.onClick.AddListener(() =>
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene(sceneNameCopy);
            });
        }
    }
}
