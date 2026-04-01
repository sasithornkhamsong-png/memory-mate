using UnityEngine;
using UnityEngine.UI;

public class AvatarSelector : MonoBehaviour
{
    public Image avatarPreview;
    public GameObject avatarPanel;

    public void SelectAvatar(Sprite newAvatar)
    {
        avatarPreview.sprite = newAvatar;

        // บันทึกชื่อ sprite
        PlayerPrefs.SetString("PlayerAvatar", newAvatar.name);
        PlayerPrefs.Save();

        avatarPanel.SetActive(false);
    }
}
