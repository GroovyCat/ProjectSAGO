using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PhotonInit : MonoBehaviourPunCallbacks
{
    void Start()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("���� ���� ����");
        PhotonNetwork.JoinOrCreateRoom("MainRoom", new RoomOptions { MaxPlayers = 2 }, TypedLobby.Default);
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("�� ���� ����: " + PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogWarning("�� ���� ����: " + message);
    }
}