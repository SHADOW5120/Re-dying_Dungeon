using TMPro;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;

public class Authentication : MonoBehaviour
{ 
    [Header("Login")]
    [SerializeField] TMP_InputField emailLoginInput;
    [SerializeField] TMP_InputField passLoginInput;
    [SerializeField] GameObject loginPage;

    [Header("Register")]
    [SerializeField] TMP_InputField usernameRegisterInput;
    [SerializeField] TMP_InputField emailRegisterInput;
    [SerializeField] TMP_InputField passRegisterInput;
    [SerializeField] GameObject registerPage;

    [Header("ForgotPass")]
    [SerializeField] TMP_InputField emailForgotPassInput;
    [SerializeField] GameObject forgotPassPage;

    [Header("ForgotPass")]
    [SerializeField] TMP_InputField guestUsernameInput;
    [SerializeField] GameObject guestPage;

    [Header("Notifications")]
    [SerializeField] GameObject errorController;
    [SerializeField] GameObject loadingController;

    public void RegisterUser()
    {
        loadingController.GetComponent<LoadingManager>().OpenLoading("Loading.....");
        var request = new RegisterPlayFabUserRequest
        {
            DisplayName = usernameRegisterInput.text,
            Email = emailRegisterInput.text,
            Password = passRegisterInput.text,

            RequireBothUsernameAndEmail = false
        };

        PlayFabClientAPI.RegisterPlayFabUser(request, OnRegisterSucess, OnError);
    }

    public void Login()
    {
        loadingController.GetComponent<LoadingManager>().OpenLoading("Loading.....");
        var request = new LoginWithEmailAddressRequest
        {
            Email = emailLoginInput.text,
            Password = passLoginInput.text,
        };

        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnError);
    }

    public void RecoverUser()
    {
        loadingController.GetComponent<LoadingManager>().OpenLoading("Loading.....");
        var request = new SendAccountRecoveryEmailRequest
        {
            Email = emailForgotPassInput.text,
            TitleId = "195D74",
        };

        PlayFabClientAPI.SendAccountRecoveryEmail(request, OnRecoverSuccess, OnError);
    }

    public void GuestUser()
    {
        PlayerModel.Initialize(guestUsernameInput.text, "Your are playing as guest!");
        Debug.Log("Play as Guest Successfully");
        ASyncLoader.Instance.LoadScene(1);
    }

    private void OnRecoverSuccess(SendAccountRecoveryEmailResult result)
    {
        loadingController.GetComponent<LoadingManager>().CloseLoading();
        OpenLoginPage();
    }

    private void OnLoginSuccess(LoginResult result)
    {
        loadingController.GetComponent<LoadingManager>().CloseLoading();
        Debug.Log("Login Successfully");
        ASyncLoader.Instance.LoadScene(1);
    }

    private void OnError(PlayFabError error)
    {
        loadingController.GetComponent<LoadingManager>().CloseLoading();
        errorController.GetComponent<ErrorManager>().ChangeErrorContent("Some errors has happened!", "You may encounter some errors when authenticating");
        errorController.GetComponent<ErrorManager>().OpenErrorPanel();
    }

    private void OnRegisterSucess(RegisterPlayFabUserResult result)
    {
        loadingController.GetComponent<LoadingManager>().CloseLoading();
        OpenLoginPage();
    }

    public void OpenLoginPage()
    {
        loginPage.SetActive(true);
        registerPage.SetActive(false);
        forgotPassPage.SetActive(false);
        guestPage.SetActive(false);
    }

    public void OpenRegisterPage()
    {
        loginPage.SetActive(false);
        registerPage.SetActive(true);
        forgotPassPage.SetActive(false);
        guestPage.SetActive(false);
    }

    public void OpenForgotPassPage()
    {
        loginPage.SetActive(false);
        registerPage.SetActive(false);
        forgotPassPage.SetActive(true);
        guestPage.SetActive(false);
    }

    public void OpenGuestPage()
    {
        loginPage.SetActive(false);
        registerPage.SetActive(false);
        forgotPassPage.SetActive(false);
        guestPage.SetActive(true);
    }
}
