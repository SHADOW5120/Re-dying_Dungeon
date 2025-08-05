using ExitGames.Client.Photon;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGameController : MonoBehaviourPunCallbacks
{
    [SerializeField] private GamePlayViewController gamePlayViewController;
    [SerializeField] private AudioClip mainMusic;
    public GameObject errorController;
    public GameObject loadingController;

    public override void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    public override void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }

    private void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == (byte)1)
        {
            int winnerID = (int)photonEvent.CustomData;

            if (PhotonNetwork.LocalPlayer.ActorNumber != winnerID)
            {
                gamePlayViewController.OpenLoseView();
            }
            else
            {
                gamePlayViewController.OpenWinView();
            }
        }
    }

    public void OnEndButton()
    {
        SoundManager.instance.ChangeMusic(mainMusic);
        ASyncLoader.Instance.LoadScene(1);
        PhotonNetwork.LeaveRoom();
    }
}
