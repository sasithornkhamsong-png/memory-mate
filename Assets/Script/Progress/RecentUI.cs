using UnityEngine;

public class RecentUI : MonoBehaviour
{
    public RecentItemUI[] items;

    public Sprite greatSprite;
    public Sprite goodSprite;

    public Color emptyColor = Color.gray;

    void Start()
    {
        LoadRealData();
    }

    void LoadRealData()
    {
        var data = GameManager.instance.recentResults;

        for (int i = 0; i < items.Length; i++)
        {
            if (i < data.Count)
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