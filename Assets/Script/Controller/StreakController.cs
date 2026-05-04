using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StreakController : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI streakText; // ตัวเลขใน badge

    void Start()
    {
        CheckStreak();
    }

    void CheckStreak()
    {
        string lastLoginStr = PlayerPrefs.GetString("LastLoginDate", "");
        int currentStreak = PlayerPrefs.GetInt("StreakCount", 0);

        DateTime today = DateTime.Now.Date;

        if (lastLoginStr == "")
        {
            // เข้าครั้งแรกเลย
            currentStreak = 1;
        }
        else
        {
            DateTime lastLogin = DateTime.Parse(lastLoginStr);
            int dayDiff = (today - lastLogin).Days;

            if (dayDiff == 0)
            {
                // เข้ามาแล้ววันนี้ ไม่ต้องทำอะไร
            }
            else if (dayDiff == 1)
            {
                // เข้ามาวันถัดไป เพิ่ม streak
                currentStreak++;
            }
            else
            {
                // หายไปมากกว่า 1 วัน reset
                currentStreak = 1;
            }
        }

        // บันทึกค่า
        PlayerPrefs.SetInt("StreakCount", currentStreak);
        PlayerPrefs.SetString("LastLoginDate", today.ToString("yyyy-MM-dd"));
        PlayerPrefs.Save();

        // อัปเดต UI
        if (streakText != null)
            streakText.text = currentStreak.ToString();

        Debug.Log("Streak วันนี้: " + currentStreak);
    }

    // เรียกใช้ตรงนี้ถ้าอยากรู้ streak ปัจจุบัน
    public int GetStreak()
    {
        return PlayerPrefs.GetInt("StreakCount", 0);
    }
}