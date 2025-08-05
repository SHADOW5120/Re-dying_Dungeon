using PlayFab;
using PlayFab.ClientModels;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

public class PlayFabDataManagement : MonoBehaviour
{
    // Here user data is used to save personal data
    // Title data is to change logic of the game
    // Use json for save 

    [SerializeField] public List<CharacterSO> heroes;
    public ShopControllder shopControllder;
    public GameObject errorController;
    public GameObject loadingController;

    public void GetData()
    {
        loadingController.GetComponent<LoadingManager>().OpenLoading("Getting heroes data.....");
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnHeroDataReceived, OnError);
    }

    private void OnHeroDataReceived(GetUserDataResult result)
    {
        loadingController.GetComponent<LoadingManager>().CloseLoading();
         List<HeroModel> heroList = JsonConvert.DeserializeObject<List<HeroModel>>(result.Data["Hero"].Value);
         shopControllder.UpdateHeroUI(heroList);
    }

    public void UpdateData()
    {
        UpdateHero();
    }

    public void UpdateHero()
    {
        List<HeroModel> heroList = new List<HeroModel>();
        foreach (var hero in heroes)
        {
            heroList.Add(hero.ToModel());
        }
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"Hero", JsonConvert.SerializeObject(heroList)}
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSent, OnError);

    }

    public void GetTitleData()
    {
        PlayFabClientAPI.GetTitleData(new GetTitleDataRequest(), OnTitleDataReceived, OnError);
    }

    private void OnTitleDataReceived(GetTitleDataResult result)
    {
        if(result.Data == null)
        {
            // Add more condition like result.containkey("...") == false)
            Debug.Log("Nothing");
            return;
        }

        // Do some logic in your game here
    }

    private void OnError(PlayFabError error)
    {
        errorController.GetComponent<ErrorManager>().ChangeErrorContent("Some errors has happened!", "You may encounter some errors when getting data");
        errorController.GetComponent<ErrorManager>().OpenErrorPanel();
    }

    private void OnDataSent(UpdateUserDataResult result)
    {
        errorController.GetComponent<ErrorManager>().ChangeErrorContent("Sent data successfully!", "You have sent some data.");
        errorController.GetComponent<ErrorManager>().OpenErrorPanel();
    }
}
