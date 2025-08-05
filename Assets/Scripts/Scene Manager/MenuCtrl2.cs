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
    [Header("���� ���� UI")]
    public GameObject settingUI;

    [Header("��Ƽ�÷��� UI")]
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
            Debug.LogWarning("Photon �뿡 ���� ���ӵ��� �ʾҽ��ϴ�.");
            return;
        }

        startUI.SetActive(false);
        connectionPanel.SetActive(true);
        InvokeRepeating(nameof(UpdatePlayerStatus), 0f, 1f);
    }


    void UpdatePlayerStatus()
    {
        var players = PhotonNetwork.PlayerList;

        player1StatusText.text = players.Length >= 1 ? "Player 1 ���ӵ�" : "Player 1 ��� ��...";
        player2StatusText.text = players.Length >= 2 ? "Player 2 ���ӵ�" : "Player 2 ��� ��...";

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
            player1StatusText.text = "Player 1 ���� �Ϸ�";
        }
        else if (players.Length > 1 && players[1].ActorNumber == actorNumber)
        {
            player2CharacterImage.sprite = characterSprites[characterIndex];
            player2StatusText.text = "Player 2 ���� �Ϸ�";
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

    // ���� é�� �ε� ��ư
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

    // ����
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

    // ����
    public void OnClickQuitGame()
    {
        Debug.Log("���� ����!");
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}