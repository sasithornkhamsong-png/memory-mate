using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource bgmSource;
    public AudioSource sfxSource;
    public AudioClip clickClip;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        bool bgmOn = PlayerPrefs.GetInt("BGMOn", 1) == 1;
        bool sfxOn = PlayerPrefs.GetInt("SFXOn", 1) == 1;

        bgmSource.mute = !bgmOn;
        sfxSource.mute = !sfxOn;

        if (!bgmSource.isPlaying)
            bgmSource.Play();
    }

    public void SetBGM(bool isOn)
    {
        bgmSource.mute = !isOn;
        PlayerPrefs.SetInt("BGMOn", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void SetSFX(bool isOn)
    {
        sfxSource.mute = !isOn;

        PlayerPrefs.SetInt("SFXOn", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void PlaySFX(AudioClip clip)
    {
        if (!sfxSource.mute)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlayClick()
    {
        AudioManager.instance.PlaySFX(clickClip);
    }
}