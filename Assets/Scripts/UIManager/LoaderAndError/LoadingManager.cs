using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadingManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public TMP_Text loadingText;
    public Image loadingSpinner;

    public void OpenLoading(string message)
    {
        loadingScreen.SetActive(true);
        if (loadingText != null) loadingText.text = message;
    }

    public void CloseLoading()
    {
        loadingScreen.SetActive(false);
    }

    private void Update()
    {
        if (loadingSpinner.gameObject.activeSelf && loadingSpinner != null)
        {
            loadingSpinner.transform.Rotate(Vector3.forward * -200 * Time.deltaTime);
        }
    }
}
