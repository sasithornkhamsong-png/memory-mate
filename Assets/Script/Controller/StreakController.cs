using UnityEngine;
using TMPro;
using System;

public class StreakController : MonoBehaviour
{
    public static StreakController instance;

    public TextMeshProUGUI streakText;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    void Start()
    {
        UpdateUI();
    }

    // เรียกตอนจบเกม ได้คะแนนอะไรก็ได้
    public void AddStreak()
    {
        string lastLoginStr = PlayerPrefs.GetString("LastStreakDate", "");
        int currentStreak = PlayerPrefs.GetInt("StreakCount", 0);

        DateTime today = DateTime.Now.Date;

        if (lastLoginStr == "")
        {
            currentStreak = 1;
        }
        else
        {
            DateTime lastDate = DateTime.Parse(lastLoginStr);
            int dayDiff = (today - lastDate).Days;

            if (dayDiff == 0)
            {
                // เล่นแล้ววันนี้ ไม่เพิ่ม
                UpdateUI();
                return;
            }
            else if (dayDiff == 1)
            {
                currentStreak++;
            }
            else
            {
                currentStreak = 1;
            }
        }

        PlayerPrefs.SetInt("StreakCount", currentStreak);
        PlayerPrefs.SetString("LastStreakDate", today.ToString("yyyy-MM-dd"));
        PlayerPrefs.Save();

        UpdateUI();
    }

    void UpdateUI()
    {
        int streak = PlayerPrefs.GetInt("StreakCount", 0);
        if (streakText != null)
            streakText.text = streak.ToString();
    }

    public int GetStreak()
    {
        return PlayerPrefs.GetInt("StreakCount", 0);
    }
}