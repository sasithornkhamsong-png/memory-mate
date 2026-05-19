using UnityEngine;
using UnityEngine.UI;

public class AvatarSelector : MonoBehaviour
{
    public Image avatarPreview;
    public GameObject avatarPanel;
    public Sprite[] avatarOptions;

    void Start()
    {
        //โหลด avatar ที่เคยเลือกไว้
        string savedName = PlayerPrefs.GetString("PlayerAvatar", "");
        if (savedName != "")
        {
            Sprite found = System.Array.Find(avatarOptions, s => s.name == savedName);
            if (found != null)
                avatarPreview.sprite = found;
        }
    }

    public void SelectAvatar(Sprite newAvatar)
    {
        avatarPreview.sprite = newAvatar;

        // บันทึกชื่อ sprite
        PlayerPrefs.SetString("PlayerAvatar", newAvatar.name);
        PlayerPrefs.Save();

        avatarPanel.SetActive(false);
    }
}
