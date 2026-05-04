using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public QuestItem currentQuest;

    public int stars = 0;

    public TextMeshProUGUI starText;
    public int lastScore;
    public int bestScore = 0;
    public int score;

    public List<int> recentResults = new List<int>();

    public RecentItemUI[] items;

    // Progress
    public Sprite greatSprite;
    public Sprite goodSprite;

    public Color emptyColor = Color.gray;


    void Awake()
    {
        instance = this;
    }

    public float bestTime = 0f;

    public void UpdateBestTime(float time)
    {
        if (bestTime == 0 || time < bestTime)
            bestTime = time;
    }

    /*void Start()
    {       
        bestScore = 90;
        lastScore = 30; // ลองเปลี่ยนค่าเล่น

        FindObjectOfType<CheerUpUI>().UpdateUI();
        //UpdateUI();

    }*/
    void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore_HouseGame", 0);
        bestTime = PlayerPrefs.GetFloat("BestTime_HouseGame", 0f);

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

    public void AddStar(int amount)
    {
        stars += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        starText.text = stars.ToString();
    }

    public void FinishGame(int score)
    {
        bool isNewBest = score > bestScore;

        if (isNewBest)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore_HouseGame", bestScore);
            PlayerPrefs.Save();
        }

        int result = isNewBest ? 2 : 1;
        recentResults.Insert(0, result);

        if (recentResults.Count > 5)
            recentResults.RemoveAt(5);

        ProgressData.instance.UpdateBestScore("HouseGame", score);
    }

    
}