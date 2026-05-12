using UnityEngine;
using UnityEngine.UI;

public class BackgroundManager : MonoBehaviour
{
    public Image bgImage;

    [Header("House Game")]
    public Sprite storyBG;
    public Sprite tableBG;
    public Sprite seizumzeeBG; // เพิ่ม

    [Header("ProMaid")]
    public Sprite shelfBG;    // เพิ่ม
    public Sprite partyBG;
    public Sprite matchingBG; // เพิ่ม

    [Header("Happy Market")]
    public Sprite marketBG;
    public Sprite shoppingBG; // เพิ่ม
    public Sprite sortingBG;  // เพิ่ม

    public void SetBG(Sprite bg)
    {
        bgImage.sprite = bg;
    }
}