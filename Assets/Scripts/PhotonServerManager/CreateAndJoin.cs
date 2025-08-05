using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class CreateAndJoin : MonoBehaviourPunCallbacks
{
    public TMP_InputField createRoom;
    public TMP_InputField joinRoom;
    public GameObject loadingController;
    public GameObject errorController;
    public MainMenuController mainMenuController;

    public void CreateRoom()
    {
        loadingController.GetComponent<LoadingManager>().OpenLoading("Creating your room.....");
        PhotonNetwork.CreateRoom(createRoom.text, new Photon.Realtime.RoomOptions() {MaxPlayers = 4, IsVisible = true, IsOpen = true, BroadcastPropsChangeToAll = true}, TypedLobby.Default, null);
    }

    public void JoinRoom()
    {
        loadingController.GetComponent<LoadingManager>().OpenLoading("Joining room.....");
        PhotonNetwork.JoinRoom(joinRoom.text);
        
    }

    public void JoinRoonInList(string roomName)
    {
        loadingController.GetComponent<LoadingManager>().OpenLoading("Joining room.....");
        PhotonNetwork.JoinRoom(roomName);
    }

    //public override void OnCreatedRoom()
    //{
    //    loadingController.GetComponent<LoadingManager>().CloseLoading();
    //    mainMenuController.OpenLobbyPage();
    //}

    public override void OnJoinedRoom()
    {
        loadingController.GetComponent<LoadingManager>().CloseLoading();
        mainMenuController.OpenLobbyPage();
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        loadingController.GetComponent<LoadingManager>().CloseLoading();
        errorController.GetComponent<ErrorManager>().ChangeErrorContent("Some errors has happened!", "Can't create or join room, something bad happened!");
        errorController.GetComponent<ErrorManager>().OpenErrorPanel();
    }
}
