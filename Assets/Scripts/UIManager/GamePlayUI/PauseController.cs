using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider soundSlider;

    private void Start()
    {
        musicSlider.onValueChanged.AddListener(ChangeMusic);
        soundSlider.onValueChanged.AddListener(ChangeSound);

        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 1f);
        soundSlider.value = PlayerPrefs.GetFloat("soundVolume", 1f);
    }

    public void ChangeSound(float _changeValue)
    {
        SoundManager.instance.UpdateSound(_changeValue);
    }

    public void ChangeMusic(float _changeValue)
    {
        SoundManager.instance.UpdateMusic(_changeValue);
    }
}
