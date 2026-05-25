using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ShowStat_Game1 : MonoBehaviour
{
    [Header("Top 1-3 (Separate Texts)")]
    public TextMeshProUGUI scoreText1;
    public TextMeshProUGUI timeText1;
    public TextMeshProUGUI scoreText2;
    public TextMeshProUGUI timeText2;
    public TextMeshProUGUI scoreText3;
    public TextMeshProUGUI timeText3;

    [Header("Rank 4-5 (Combined Texts)")]
    public TextMeshProUGUI rank4Text;
    public TextMeshProUGUI rank5Text;

    void Start()
    {
        DisplayStats();
    }

    void DisplayStats()
    {
        // ดึงข้อมูลจาก Manager ต้องเป็นชื่อคลาสเดียวกับสคริปต์ที่จัดการคะแนน
        List<ScoreEntry> topScores = scoreGame1Manager.GetTopScores();

        // --- จัดการลำดับที่ 1-3 ---
        SetSeparateText(0, scoreText1, timeText1, topScores);
        SetSeparateText(1, scoreText2, timeText2, topScores);
        SetSeparateText(2, scoreText3, timeText3, topScores);

        // --- จัดการลำดับที่ 4-5 ---
        SetCombinedText(3, rank4Text, topScores);
        SetCombinedText(4, rank5Text, topScores);
    }

    // สำหรับลำดับ 1-3
    void SetSeparateText(int index, TextMeshProUGUI sText, TextMeshProUGUI tText, List<ScoreEntry> list)
    {
        if (sText == null || tText == null) return;

        if (index < list.Count)
        {
            ScoreEntry s = list[index];
            int min = Mathf.FloorToInt(s.time / 60);
            int sec = Mathf.FloorToInt(s.time % 60);

            sText.text = s.score.ToString();
            tText.text = string.Format("{0:00}:{1:00}", min, sec);
        }
        else
        {
            sText.text = "-";
            tText.text = "--:--";
        }
    }

    // สำหรับลำดับ 4-5
    void SetCombinedText(int index, TextMeshProUGUI cText, List<ScoreEntry> list)
    {
        if (cText == null) return;

        if (index < list.Count)
        {
            ScoreEntry s = list[index];
            int min = Mathf.FloorToInt(s.time / 60);
            int sec = Mathf.FloorToInt(s.time % 60);

            // แสดงคะแนน และ เวลา
            cText.text = string.Format("  Score: {0} | Time: {1:00}:{2:00}", 
                 s.score, min, sec);
        }
        else
        {
            cText.text = (index + 1) + ". --- No Record ---";
        }
    }

    public void BackToMenu()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void GoToTop5Game2()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("stat_game2");
    }

    public void GoToTop5Game3()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("stat_game3");
    }


}