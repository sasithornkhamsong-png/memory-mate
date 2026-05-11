using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    public void PlayClick()
    {
        AudioManager.instance.PlaySFX(AudioManager.instance.clickClip);
    }
}