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

    [Header("멀티플레이 UI")]
    public GameObject startUI;
    public GameObject connectionPanel;
    public GameObject characterSelectPanel;

    public TextMeshProUGUI player1StatusText;
    public TextMeshProUGUI player2StatusText;

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

        player1StatusText.text = players.Length >= 1 ? "Player 1 접속됨" : "Player 1 대기 중...";
        player2StatusText.text = players.Length >= 2 ? "Player 2 접속됨" : "Player 2 대기 중...";

        if (players.Length == 2)
        {
            connectionPanel.SetActive(false);
            characterSelectPanel.SetActive(true);
            CancelInvoke(nameof(UpdatePlayerStatus));
        }
    }

    public void OnClickCharacterSelect(int characterIndex)
    {
        if (selectedCharacters.ContainsValue(characterIndex)) return;

        photonView.RPC("RPC_SelectCharacter", RpcTarget.AllBuffered, PhotonNetwork.LocalPlayer.ActorNumber, characterIndex);
    }

    [PunRPC]
    void RPC_SelectCharacter(int actorNumber, int characterIndex)
    {
        selectedCharacters[actorNumber] = characterIndex;

        var players = PhotonNetwork.PlayerList;
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

        CheckGameStartReady();
    }

    void CheckGameStartReady()
    {
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

    // 기존 챕터 로딩 버튼
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

    // 설정
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

    // 종료
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