using UnityEngine;

public class RecentUI : MonoBehaviour
{
    public RecentItemUI[] items;

    public Sprite greatSprite;
    public Sprite goodSprite;

    public Color emptyColor = Color.gray;

    public string gameName; // ใส่ใน Inspector

    void Start()
    {
        LoadRealData();
    }

    void LoadRealData()
    {
        for (int i = 0; i < items.Length; i++)
        {
            int result = PlayerPrefs.GetInt(gameName + "_Recent_" + i, 0);

            if (result == 2)
                items[i].SetGreat(greatSprite);
            else if (result == 1)
                items[i].SetGood(goodSprite);
            else
                items[i].SetEmpty(emptyColor);
        }
    }
}