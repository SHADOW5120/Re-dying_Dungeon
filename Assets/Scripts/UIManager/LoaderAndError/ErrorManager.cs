using TMPro;
using UnityEngine;

public class ErrorManager : MonoBehaviour
{
    public TMP_Text errorHeader;
    public TMP_Text errorContent;
    public GameObject errorPanel;

    public void ChangeErrorContent(string header, string content)
    {
        errorHeader.text = header;
        errorContent.text = content;
    }

    public void OpenErrorPanel()
    {
        errorPanel.SetActive(true);
    }

    public void CloseErrorPanel()
    {
        errorPanel.SetActive(false);
    }
}
