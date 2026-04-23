using UnityEngine;
using UnityEngine.UI;

public class RecentItemUI : MonoBehaviour
{
    public Image icon;
    public Image background;

    public void SetEmpty(Color emptyColor)
    {
        icon.gameObject.SetActive(false);
        background.color = emptyColor;
    }

    public void SetGreat(Sprite sprite)
    {
        icon.gameObject.SetActive(true);
        icon.sprite = sprite;
        background.color = Color.white;
    }

    public void SetGood(Sprite sprite)
    {
        icon.gameObject.SetActive(true);
        icon.sprite = sprite;
        background.color = Color.white;
    }
}