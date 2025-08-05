using PlayFab;
using UnityEngine;

public class LogoutManager : MonoBehaviour
{
    public void Logout()
    {
        PlayFabClientAPI.ForgetAllCredentials();

        PlayerModel.Destroy();

        ASyncLoader.Instance.LoadScene(0);
    }
}
