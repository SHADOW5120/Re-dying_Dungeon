using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ASyncLoader : MonoBehaviour
{
    public static ASyncLoader Instance;

    public GameObject loadingScreen;
    public GameObject loadingBackground;
    public Image loadingSlider;
    public TMP_Text contentText;
    public TMP_Text loadingText;
    public Sprite[] backgrounds;

    private string[] content = {"Login may be failed sometimes because of lagging", "If you have any trouble, just come to contact the devs", "Say hello to your darkest journal :))" };

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void LoadScene(int sceneID)
    {
        loadingBackground.GetComponent<Image>().sprite = backgrounds[Random.Range(0, backgrounds.Length)];
        contentText.text = content[Random.Range(0, content.Length)];
        StartCoroutine(LoadSceneAsync(sceneID));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        loadingScreen.SetActive(true);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            loadingSlider.fillAmount = progressValue;
            loadingText.text = (progressValue * 100).ToString("F0") + "%";

            yield return null;
        }

        yield return new WaitForSeconds(1f);

        loadingScreen.SetActive(false);
    }

    public void LoadData(string dataContent, float timer)
    {
        loadingBackground.GetComponent<Image>().sprite = backgrounds[Random.Range(0, backgrounds.Length)];
        contentText.text = dataContent;
        StartCoroutine(LoadProgress(timer));
    }

    private IEnumerator LoadProgress(float timer)
    {
        loadingScreen.SetActive(true);

        float progress = 0f;

        while (progress <= timer)
        {
            progress += Time.deltaTime;
            float fillValue = Mathf.Clamp01(progress / timer);
            loadingSlider.fillAmount = fillValue;
            loadingText.text = (fillValue * 100f).ToString("F0") + "%";
            yield return null;
        }

        loadingScreen.SetActive(false);
    }
}
