using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    [Header("UI 패널 연결")]
    public GameObject tutorialPanel;
    public GameObject settingsPanel;
    public Text tutorialText;
    public KeyHighlightUI keyUI;
    public string[] tutorialSteps;

    private int currentStep = 0;
    private bool tutorialActive = true;

    void Start()
    {
        StartTutorial(); // 무조건 시작할 때 튜토리얼 시작
        settingsPanel.SetActive(false);
    }

    void Update()
    {
        if (!tutorialActive)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                settingsPanel.SetActive(!settingsPanel.activeSelf);
            }
            return;
        }

        // ESC로 튜토리얼 끄기
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseTutorial();
            return;
        }

        // 튜토리얼 문장 갱신
        if (currentStep < tutorialSteps.Length)
        {
            tutorialText.text = tutorialSteps[currentStep];

            switch (currentStep)
            {
                case 0: if (Input.GetKeyDown(KeyCode.W)) AdvanceStep(); break;
                case 1: if (Input.GetKeyDown(KeyCode.A)) AdvanceStep(); break;
                case 2: if (Input.GetKeyDown(KeyCode.S)) AdvanceStep(); break;
                case 3: if (Input.GetKeyDown(KeyCode.D)) AdvanceStep(); break;
                case 4: if (Input.GetKeyDown(KeyCode.Space)) AdvanceStep(); break;
                case 5: if (Input.GetKeyDown(KeyCode.E)) AdvanceStep(); break;
            }
        }
    }

    public void StartTutorial()
    {
        tutorialActive = true;
        currentStep = 0;
        tutorialPanel.SetActive(true);
        tutorialText.text = tutorialSteps.Length > 0 ? tutorialSteps[0] : "";
        keyUI.enabled = true;
        settingsPanel.SetActive(false);
    }

    public void CloseTutorial()
    {
        tutorialActive = false;
        tutorialPanel.SetActive(false);
        keyUI.enabled = false;
    }

    public void OnSettingsOpenTutorial()
    {
        StartTutorial();
    }

    void AdvanceStep()
    {
        currentStep++;
        if (currentStep >= tutorialSteps.Length)
        {
            CloseTutorial();
        }
    }
}
