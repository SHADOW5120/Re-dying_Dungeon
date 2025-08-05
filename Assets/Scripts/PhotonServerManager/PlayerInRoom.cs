using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class PlayerInRoom : MonoBehaviourPunCallbacks
{
    public TMP_Text playerName;
    Image backgroundImg;
    public Color hightlightColor;
    public GameObject leftButton;
    public GameObject rightButton;

    public Image playerAvatar;
    public Sprite[] avatars;

    Player player;

    ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable();

    private void Awake()
    {
        backgroundImg = GetComponent<Image>();
    }

    public void SetPlayerInfor(Player _player)
    {
        player = _player;
        playerName.text = player.NickName;
        UpdatePlayerInRoom(player);
    }

    public void ApplyLocalChanges()
    {
        backgroundImg.color = hightlightColor;
        leftButton.SetActive(true);
        rightButton.SetActive(true);
    }

    public void OnClickLeftButton()
    {
        if ((int)playerProperties["playerAvatar"] == 0)
        {
            playerProperties["playerAvatar"] = avatars.Length - 1;
        }
        else
        {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] - 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public void OnClickRightButton()
    {
        if ((int)playerProperties["playerAvatar"] == avatars.Length - 1)
        {
            playerProperties["playerAvatar"] = 0;
        }
        else
        {
            playerProperties["playerAvatar"] = (int)playerProperties["playerAvatar"] + 1;
        }
        PhotonNetwork.SetPlayerCustomProperties(playerProperties);
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {
        if(player == targetPlayer)
        {
            UpdatePlayerInRoom(targetPlayer);
        }
    }

    private void UpdatePlayerInRoom(Player player)
    {
        if (player.CustomProperties.ContainsKey("playerAvatar"))
        {
            playerAvatar.sprite = avatars[(int)player.CustomProperties["playerAvatar"]];
            playerProperties["playerAvatar"] = (int)player.CustomProperties["playerAvatar"];
            Debug.Log((int)player.CustomProperties["playerAvatar"]);
        }
        else
        {
            playerProperties["playerAvatar"] = 0;
        }
    }
}
