using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource soundSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        LoadVolumeSettings();
    }

    public void PlaySound(AudioClip _audio)
    {
        soundSource.PlayOneShot(_audio);
    }

    public void UpdateSound(float _changeValue)
    {
        ChangeSourceVolume("soundVolume", soundSource, _changeValue);
    }

    public void UpdateMusic(float _changeValue)
    {
        ChangeSourceVolume("musicVolume", musicSource, _changeValue);
    }

    private void ChangeSourceVolume(string volumeName, AudioSource source, float _value)
    {
        source.volume = _value;
        PlayerPrefs.SetFloat(volumeName, _value);
        PlayerPrefs.Save();
    }

    private void LoadVolumeSettings()
    {
        float soundVolume = PlayerPrefs.GetFloat("soundVolume", 1f);
        float musicVolume = PlayerPrefs.GetFloat("musicVolume", 1f);

        soundSource.volume = soundVolume;
        musicSource.volume = musicVolume;
    }

    public void ChangeMusic(AudioClip newMusic)
    {
        if (musicSource.clip == newMusic) return;

        musicSource.clip = newMusic;
        musicSource.Play();
    }
}
