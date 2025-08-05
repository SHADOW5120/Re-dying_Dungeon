using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    public InputField messageTextHolder;
    public GameObject message;
    public GameObject content;

    public void SendMessage()
    {
        GetComponent<PhotonView>().RPC("GetMessage", RpcTarget.All, messageTextHolder.text);
        messageTextHolder.text = "";
    }

    [PunRPC]
    public void GetMessage(string receiveMessage)
    {
        GameObject temp = Instantiate(message, Vector3.zero, Quaternion.identity, content.transform);
        temp.GetComponent<Message>().myMessage.text = receiveMessage;
    }
}
