using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [Header("BGM")]
    public Toggle bgmToggle;
    public AudioSource bgmSource;

    public Image bgmBackground;
    public RectTransform bgmKnob;

    [Header("SFX")]
    public Toggle sfxToggle;
    public AudioSource sfxSource;

    public Image sfxBackground;
    public RectTransform sfxKnob;

    [Header("Toggle Style")]
    public Color onColor = new Color(0.2f, 0.8f, 0.2f);
    public Color offColor = new Color(0.6f, 0.6f, 0.6f);

    public float knobOnX = -10f;
    public float knobOffX = -125f;

    void Start()
    {
        // ===== BGM =====
        bool bgmState =
            PlayerPrefs.GetInt("BGMOn", 1) == 1;

        bgmToggle.isOn = bgmState;

        ApplyToggleVisual(
            bgmBackground,
            bgmKnob,
            bgmState
        );

        bgmSource.mute = !bgmState;

        if (!bgmSource.isPlaying)
            bgmSource.Play();

        // ===== SFX =====
        bool sfxState =
            PlayerPrefs.GetInt("SFXOn", 1) == 1;

        sfxToggle.isOn = sfxState;

        ApplyToggleVisual(
            sfxBackground,
            sfxKnob,
            sfxState
        );

        sfxSource.mute = !sfxState;
    }

    // =========================
    // BGM
    // =========================
    public void ToggleBGM(bool isOn)
    {
        AudioManager.instance.SetBGM(isOn);

        ApplyToggleVisual(
            bgmBackground,
            bgmKnob,
            isOn
        );

        PlayerPrefs.SetInt("BGMOn", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }

    // =========================
    // SFX
    // =========================
    public void ToggleSFX(bool isOn)
    {
        sfxSource.mute = !isOn;

        ApplyToggleVisual(
            sfxBackground,
            sfxKnob,
            isOn
        );

        PlayerPrefs.SetInt(
            "SFXOn",
            isOn ? 1 : 0
        );

        PlayerPrefs.Save();
    }

    // =========================
    // Visual
    // =========================
    void ApplyToggleVisual(
        Image bg,
        RectTransform knob,
        bool isOn
    )
    {
        bg.color = isOn ? onColor : offColor;

        knob.anchoredPosition =
            new Vector2(
                isOn ? knobOnX : knobOffX,
                0f
            );
    }
}