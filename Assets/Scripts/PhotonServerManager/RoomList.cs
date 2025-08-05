using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;
using UnityEngine;

public class RoomList : MonoBehaviourPunCallbacks
{
    public GameObject roomPrefab;
    public GameObject[] allRooms;
    public Transform contentView;
    public GameObject loadingController;

    private float timeBetweenUpdate = 0.5f;
    private float timeNext;

    private void Start()
    {
        loadingController.GetComponent<LoadingManager>().OpenLoading("Connecting to Server.....");
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        loadingController.GetComponent<LoadingManager>().CloseLoading();
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(Photon.Realtime.DisconnectCause cause)
    {
        loadingController.GetComponent<LoadingManager>().OpenLoading("Disconnecting! Try to connect again.....");
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        PhotonNetwork.NickName = PlayerModel.Instance.username;
        if (Time.time >= timeNext)
        {
            for (int i = 0; i < allRooms.Length; i++)
            {
                if (allRooms[i] != null)
                {
                    Destroy(allRooms[i]);
                }
            }

            allRooms = new GameObject[roomList.Count];

            for (int i = 0; i < roomList.Count; i++)
            {
                Debug.Log(roomList[i].Name);
                if (roomList[i].IsOpen && roomList[i].IsVisible && roomList[i].PlayerCount >= 1)
                {
                    GameObject tempRoom = Instantiate(roomPrefab, Vector3.zero, Quaternion.identity, contentView);
                    tempRoom.GetComponent<OnlRoom>().roomName.text = roomList[i].Name;

                    allRooms[i] = tempRoom;
                }

            }
            timeNext = Time.time + timeBetweenUpdate;
        }
    }
}
