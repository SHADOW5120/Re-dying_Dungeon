using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LobbyController : MonoBehaviourPunCallbacks
{
    public List<GameObject> playerInRooms = new List<GameObject>();
    public GameObject playerInRoom;
    public TMP_Text lobbyName;
    public Transform contentView;
    public GameObject playButton;
    public GameObject leaveButton;
    public GameObject errorController;
    public GameObject loadingController;

    private void Start()
    {
        if (PhotonNetwork.CurrentRoom.Name != null)
        {
            lobbyName.text = "Room: " + PhotonNetwork.CurrentRoom.Name;
        }
        PlayerListUpdate();
    }

    public void PlayerListUpdate()
    {
        foreach (GameObject player in playerInRooms)
        {
            Destroy(player);
        }

        playerInRooms.Clear();

        if (PhotonNetwork.CurrentRoom == null) return;

        foreach (KeyValuePair<int, Player> player in PhotonNetwork.CurrentRoom.Players)
        {
            GameObject newPlayerInRoom = Instantiate(playerInRoom, Vector3.zero, Quaternion.identity, contentView);

            var playerScript = newPlayerInRoom.GetComponentInChildren<PlayerInRoom>();

            playerScript.SetPlayerInfor(player.Value);

            if (player.Value == PhotonNetwork.LocalPlayer)
            {
                playerScript.ApplyLocalChanges();
            }

            playerInRooms.Add(newPlayerInRoom);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        PlayerListUpdate();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        PlayerListUpdate();
    }

    //public override void OnJoinedRoom()
    //{
    //    if (PhotonNetwork.CurrentRoom.Name != null)
    //    {
    //        lobbyName.text = "Room: " + PhotonNetwork.CurrentRoom.Name;
    //    }
    //    PlayerListUpdate();
    //}

    public override void OnLeftRoom()
    {
        loadingController.GetComponent<LoadingManager>().CloseLoading();
    }

    private void Update()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount >= 1)
        {
            playButton.SetActive(true);
        }
        else
        {
            playButton.SetActive(false);
        }
    }

    public void OnClickLeaveButton()
    {
        loadingController.GetComponent<LoadingManager>().OpenLoading("Leaving.....");
        PhotonNetwork.LeaveRoom();
    }

    public void OnClickPlayButton()
    {
        loadingController.GetComponent<LoadingManager>().OpenLoading("Please wait a minute!");
        PhotonNetwork.LoadLevel("GamePlay");
    }
}
