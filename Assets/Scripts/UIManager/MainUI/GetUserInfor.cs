using PlayFab.ClientModels;
using PlayFab;
using UnityEngine;

public class GetUserInfor : MonoBehaviour
{
    public GameObject errorController;
    public GameObject loadingController;

    private void Start()
    {
        GetPlayerData();
    }

    public void GetPlayerData()
    {
        if(PlayerModel.Instance.username == null)
        {
            loadingController.GetComponent<LoadingManager>().OpenLoading("Initilizing data...");

            var request = new GetAccountInfoRequest();

            PlayFabClientAPI.GetAccountInfo(request, OnGetInforSuccess, OnError);
        }
    }

    private void OnError(PlayFabError error)
    {
        loadingController.GetComponent<LoadingManager>().CloseLoading();
        errorController.GetComponent<ErrorManager>().ChangeErrorContent("Some errors has happened!", "You may encounter some errors when geteting user data");
        errorController.GetComponent<ErrorManager>().OpenErrorPanel();
    }

    private void OnGetInforSuccess(GetAccountInfoResult result)
    {
        loadingController.GetComponent<LoadingManager>().CloseLoading();

        string displayName = result.AccountInfo.TitleInfo?.DisplayName;
        string email = result.AccountInfo.PrivateInfo?.Email;

        PlayerModel.Initialize(displayName, email);
    }
}
