using TMPro;
using UnityEngine;

public class OnlRoom : MonoBehaviour
{
    public TMP_Text roomName;

    public void JoinRoom()
    {
        GameObject.Find("CreateAndJoin").GetComponent<CreateAndJoin>().JoinRoonInList(roomName.text);
    }
}
