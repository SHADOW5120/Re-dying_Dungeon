using Photon.Pun;
using TMPro;
using UnityEngine;

public class UserName : MonoBehaviour
{
    public TMP_InputField username;
    public GameObject userNamePage;
    public TMP_Text myUserName;

    private void Start()
    {
        if (PlayerPrefs.GetString("Username") == "" || PlayerPrefs.GetString("Username") == null)
        {
            userNamePage.SetActive(true);
        }
        else
        {
            PhotonNetwork.NickName = PlayerPrefs.GetString("Username");

            myUserName.text = PlayerPrefs.GetString("Username");

            userNamePage.SetActive(false);
        }
    }

    public void SaveUsername()
    {
        PhotonNetwork.NickName = username.text;

        PlayerPrefs.SetString("Username", username.text);

        myUserName.text = username.text;

        userNamePage.SetActive(false);
    }
}
