using UnityEngine.UI;
using UnityEngine;

public class Message : MonoBehaviour
{
    public Text myMessage;

    private void Start()
    {
        GetComponent<RectTransform>().SetAsFirstSibling();
    }
}
