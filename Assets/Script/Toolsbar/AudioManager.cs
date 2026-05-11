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

        bgmSource.mute = !bgmOn;

        if (!bgmSource.isPlaying)
            bgmSource.Play();
    }

    public void SetBGM(bool isOn)
    {
        bgmSource.mute = !isOn;
        PlayerPrefs.SetInt("BGMOn", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayClick()
    {
        AudioManager.instance.PlaySFX(clickClip);
    }
}