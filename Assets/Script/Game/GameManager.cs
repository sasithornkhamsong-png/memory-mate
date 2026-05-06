using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // ========== ของโบ ==========
    public QuestItem currentQuest;

    public int stars = 0;
    public TextMeshProUGUI starText;

    public int lastScore;
    public int bestScore = 0;
    public int score;
    public float bestTime = 0f;

    public List<int> recentResults = new List<int>();
    public RecentItemUI[] items;

    public Sprite greatSprite;
    public Sprite goodSprite;
    public Color emptyColor = Color.gray;

    // ========== ของเพื่อน ==========
    public float myItem_time = 0f;
    public int myItem_score = 100;

    public float card_time = 0f;
    public int card_score = 100;

    public int game1_score = 0;
    public float game1_time = 0f;

    // ========== Awake ==========
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // ========== Start ==========
    void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore_HouseGame", 0);
        bestTime = PlayerPrefs.GetFloat("BestTime_HouseGame", 0f);

        recentResults.Clear();
        for (int i = 0; i < 5; i++)
        {
            int result = PlayerPrefs.GetInt("HouseGame_Recent_" + i, 0);
            if (result != 0)
                recentResults.Add(result);
        }

        LoadRealData();
    }

    // ========== ฟังก์ชันของโบ ==========
    void LoadRealData()
    {
        if (items == null || items.Length == 0) return;

        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == null) continue;
            
            if (i < recentResults.Count)
            {
                if (recentResults[i] == 2)
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

    public void UpdateBestTime(float time)
    {
        if (bestTime == 0 || time < bestTime)
            bestTime = time;
    }

    public void FinishGame(int score)
    {
        lastScore = score;
        PlayerPrefs.SetInt("HouseGame_LastScore", score);

        bool isNewBest = score > bestScore;
        if (isNewBest)
        {
            bestScore = score;
            PlayerPrefs.SetInt("BestScore_HouseGame", bestScore);
        }

        int result = isNewBest ? 2 : 1;
        recentResults.Insert(0, result);

        if (recentResults.Count > 5)
            recentResults.RemoveAt(5);

        for (int i = 0; i < recentResults.Count; i++)
            PlayerPrefs.SetInt("HouseGame_Recent_" + i, recentResults[i]);

        PlayerPrefs.Save();

        ProgressData.instance.UpdateBestScore("HouseGame", score);
        StreakController.instance.AddStreak();
    }

    // ========== ฟังก์ชันของเพื่อน ==========
    public void CalculateFinalResults()
    {
        game1_score = myItem_score + card_score;
        game1_time = myItem_time + card_time;
    }

    public void ResetGameData()
    {
        myItem_time = 0f;
        myItem_score = 100;
        card_time = 0f;
        card_score = 100;
        game1_score = 0;
        game1_time = 0f;
    }
}