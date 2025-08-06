using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class MenuCtrl2 : MonoBehaviourPunCallbacks
{
    [Header("기존 설정 UI")]
    public GameObject settingUI;

    [Header("UI 패널")]
    public GameObject startUI;
    public GameObject connectionPanel;
    public GameObject characterSelectPanel;

    [Header("접속 상태 패널")]
    public Image player1Image;  
    public Image player2Image;
    public Color connectedColor1 = new Color(0f, 1f, 1f, 1f);
    public Color connectedColor2 = new Color(1f, 1f, 0f, 1f);
    public Color disconnectedColor = new Color(0.5f, 0.5f, 0.5f, 0.3f);

    public TextMeshProUGUI player1StatusText;
    public TextMeshProUGUI player2StatusText;

    [Header("캐릭터 선택 패널")]
    public Toggle[] characterToggles;
    public Image player1CharacterImage;
    public Image player2CharacterImage;
    public Sprite[] characterSprites;
    public Button newGameButton;
    public Button loadGameButton;

    private Dictionary<int, int> selectedCharacters = new Dictionary<int, int>();

    void Start()
    {
        connectionPanel.SetActive(false);
        characterSelectPanel.SetActive(false);
    }

    public void OnClickStartMultiplayer()
    {
        if (!PhotonNetwork.InRoom)
        {
            Debug.LogWarning("Photon 룸에 아직 접속되지 않았습니다.");
            return;
        }

        startUI.SetActive(false);
        connectionPanel.SetActive(true);
        InvokeRepeating(nameof(UpdatePlayerStatus), 0f, 1f);
    }

    void UpdatePlayerStatus()
    {
        var players = PhotonNetwork.PlayerList;

        player1Image.color = players.Length >= 1 ? connectedColor1 : disconnectedColor;
        player1StatusText.text = players.Length >= 1 ? "접속 완료" : "플레이어 미접속...";

        player2Image.color = players.Length >= 2 ? connectedColor2 : disconnectedColor;
        player2StatusText.text = players.Length >= 2 ? "접속 완료" : "플레이어 미접속...";

        if (players.Length == 2)
        {
            connectionPanel.SetActive(false);
            characterSelectPanel.SetActive(true);
            CancelInvoke(nameof(UpdatePlayerStatus));
            InitializeCharacterToggles();
        }
    }

    void InitializeCharacterToggles()
    {
        foreach (var toggle in characterToggles)
        {
            toggle.interactable = true;
            toggle.isOn = false;
        }
        selectedCharacters.Clear();
        newGameButton.interactable = false;
        loadGameButton.interactable = false;
    }

    public void OnToggleCharacterSelected(int characterIndex)
    {
        if (!characterToggles[characterIndex].isOn) return;

        if (selectedCharacters.ContainsValue(characterIndex))
        {
            characterToggles[characterIndex].isOn = false;
            return;
        }

        photonView.RPC("RPC_SelectCharacter", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.ActorNumber, characterIndex);
    }

    [PunRPC]
    void RPC_SelectCharacter(int actorNumber, int characterIndex)
    {
        if (selectedCharacters.ContainsValue(characterIndex)) return;

        selectedCharacters[actorNumber] = characterIndex;

        var players = PhotonNetwork.PlayerList;

        for (int i = 0; i < characterToggles.Length; i++)
        {
            if (selectedCharacters.ContainsValue(i) && characterIndex != i)
            {
                characterToggles[i].interactable = false;
            }
        }

        if (players[0].ActorNumber == actorNumber)
        {
            player1CharacterImage.sprite = characterSprites[characterIndex];
            player1StatusText.text = "Player 1 선택 완료";
        }
        else if (players.Length > 1 && players[1].ActorNumber == actorNumber)
        {
            player2CharacterImage.sprite = characterSprites[characterIndex];
            player2StatusText.text = "Player 2 선택 완료";
        }

        if (selectedCharacters.Count == 2)
        {
            newGameButton.interactable = true;
            loadGameButton.interactable = PlayerPrefs.HasKey("SaveData");
        }
    }

    public void OnClickNewGame()
    {
        PlayerPrefs.SetInt("StartWithTutorial", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("1.Third Floor Scene");
    }

    public void OnClickLoadGame()
    {
        string scene = PlayerPrefs.GetString("SaveData", "1.Third Floor Scene");
        SceneManager.LoadScene(scene);
    }

    public void OnClickLoadChapterScene(string sceneName, int chapterIndex)
    {
        PlayerPrefs.SetString("NextScene", sceneName);
        PlayerPrefs.SetInt("SelectedChapter", chapterIndex);
        SceneManager.LoadScene("LoadingScene");
    }

    public void OnClickLoadChapter1() => OnClickLoadChapterScene("1.Third Floor Scene", 1);
    public void OnClickLoadChapter2() => OnClickLoadChapterScene("2.Maze Scene", 2);
    public void OnClickLoadChapter3() => OnClickLoadChapterScene("3.Second Floor Scene", 3);
    public void OnClickLoadChapter4() => OnClickLoadChapterScene("4.First Floor Boss Scene", 4);

    public void OpenSettings()
    {
        settingUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseSettings()
    {
        settingUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnClickRestartLikeFresh()
    {
        RestartGameLikeFresh();
    }

    public void RestartGameLikeFresh()
    {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.Save();

        if (PhotonNetwork.InRoom)
            PhotonNetwork.LeaveRoom();

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnClickQuitGame()
    {
        Debug.Log("게임 종료!");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
