using UnityEngine;

public class GlobalAudioManager : MonoBehaviour
{
    public static GlobalAudioManager instance;

    public AudioSource bgmSource;

    void Awake()
    {
        // ถ้ามีตัวอยู่แล้ว ลบตัวใหม่ทิ้ง
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;

        DontDestroyOnLoad(gameObject);

        bool bgmOn =
            PlayerPrefs.GetInt("BGMOn", 1) == 1;

        bgmSource.mute = !bgmOn;

        if (!bgmSource.isPlaying)
        {
            bgmSource.Play();
        }
    }
}