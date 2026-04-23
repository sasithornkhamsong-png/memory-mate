using UnityEngine;

public class RecentUI : MonoBehaviour
{
    public RecentItemUI[] items;

    public Sprite greatSprite;
    public Sprite goodSprite;

    public Color emptyColor = Color.gray;

    void Start()
    {
        LoadTestData();
    }

    void LoadTestData()
    {
        int[] data = { 2, 1, 1, 2 }; // ⭐ ลองก่อน

        for (int i = 0; i < items.Length; i++)
        {
            if (i < data.Length)
            {
                if (data[i] == 2)
                    items[i].SetGreat(greatSprite);
                else
                    items[i].SetGood(goodSprite);
            }
            else
            {
                items[i].SetEmpty(emptyColor);
            }
        }
    }
}