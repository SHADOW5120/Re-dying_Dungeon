using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] GameObject mainPage;
    [SerializeField] GameObject shopPage;
    [SerializeField] GameObject settingPage;
    [SerializeField] GameObject inforPage;
    [SerializeField] GameObject historyPage;
    [SerializeField] GameObject roomListPage;
    [SerializeField] GameObject lobbyPage;
    [SerializeField] AudioClip battleMusic;

    public void OpenMainPage()
    {
        mainPage.SetActive(true);
        shopPage.SetActive(false);
        settingPage.SetActive(false);
        inforPage.SetActive(false);
        historyPage.SetActive(false);
        roomListPage.SetActive(false);
        lobbyPage.SetActive(false);
    }

    public void OpenShopPage()
    {
        mainPage.SetActive(false);
        shopPage.SetActive(true);
        settingPage.SetActive(false);
        inforPage.SetActive(false);
        historyPage.SetActive(false);
        roomListPage.SetActive(false);
    }

    public void OpenSettingPage()
    {
        mainPage.SetActive(false);
        shopPage.SetActive(false);
        settingPage.SetActive(true);
        inforPage.SetActive(false);
        historyPage.SetActive(false);
        roomListPage.SetActive(false);
        lobbyPage.SetActive(false);
    }

    public void OpenInforPage()
    {
        mainPage.SetActive(false);
        shopPage.SetActive(false);
        settingPage.SetActive(false);
        inforPage.SetActive(true);
        historyPage.SetActive(false);
        roomListPage.SetActive(false);
        lobbyPage.SetActive(false);
    }

    public void OpenHistoryPage()
    {
        mainPage.SetActive(false);
        shopPage.SetActive(false);
        settingPage.SetActive(false);
        inforPage.SetActive(false);
        historyPage.SetActive(true);
        roomListPage.SetActive(false);
        lobbyPage.SetActive(false);
    }

    public void OpenRoomListPage()
    {
        mainPage.SetActive(false);
        shopPage.SetActive(false);
        settingPage.SetActive(false);
        inforPage.SetActive(false);
        historyPage.SetActive(false);
        roomListPage.SetActive(true);
        lobbyPage.SetActive(false);
    }

    public void OpenLobbyPage()
    {
        mainPage.SetActive(false);
        shopPage.SetActive(false);
        settingPage.SetActive(false);
        inforPage.SetActive(false);
        historyPage.SetActive(false);
        roomListPage.SetActive(false);
        lobbyPage.SetActive(true);
    }
}
