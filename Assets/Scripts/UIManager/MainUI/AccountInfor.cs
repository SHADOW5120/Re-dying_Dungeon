using TMPro;
using UnityEngine;

public class AccountInfor : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField email;

    public void GetAccountInfor()
    {
        username.text = PlayerModel.Instance.username;
        email.text = PlayerModel.Instance.email;
    }

    public void SetAccountInfor()
    {
        PlayerModel.Initialize(username.text, email.text);
    }
}
