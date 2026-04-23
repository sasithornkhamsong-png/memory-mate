using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    public Image bgImage;

    public Sprite storyBG;
    public Sprite marketBG;
    public Sprite partyBG;
    public Sprite tableBG;

    public void SetBG(Sprite bg)
    {
        bgImage.sprite = bg;
    }
}
