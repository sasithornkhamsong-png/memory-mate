using UnityEngine;
using UnityEngine.UI;

public class AvatarLoader : MonoBehaviour
{
    public Image avatarImage;
    public Sprite[] avatars;

    void Start()
    {
        string avatarName = PlayerPrefs.GetString("PlayerAvatar");

        foreach (Sprite avatar in avatars)
        {
            if (avatar.name == avatarName)
            {
                avatarImage.sprite = avatar;
                break;
            }
        }
    }
}