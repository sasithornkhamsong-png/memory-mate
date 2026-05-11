using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour
{
    [Header("Game Setting")]
    public string game1Name = "HouseGame";
    public string game2Name = "Promaid";
    
    [Header("UI Game 1 (House Game)")]
    public TextMeshProUGUI game1PlayText;      // ภารกิจเล่นเกมที่ 1
    public TextMeshProUGUI game1TopScoreText;  // ภารกิจได้อันดับ 1 ของเกมที่ 1

    [Header("UI Game 2")]
    public TextMeshProUGUI game2PlayText;      
    public TextMeshProUGUI game2TopScoreText;  

    void Start()
    {
        UpdateMissionUI();
    }

    void UpdateMissionUI()
    {
        //--- เกมที่ 1 ---
        DisplayMission(game1Name, "Game1_PlayCount", "Game1_TopScoreCount", game1PlayText, game1TopScoreText);
        // --- เกมที่ 2 ---
        DisplayMission(game2Name, "Mission_PlayCount", "Mission_TopScoreCount", game2PlayText, game2TopScoreText);
    }

    // แสดงภารกิจ
    void DisplayMission(string gameTitle, string playKey, string topKey, TextMeshProUGUI playUI, TextMeshProUGUI topUI)
    {
        if (playUI == null || topUI == null) return;

        // 1. ภารกิจเล่นครบ 3 ครั้ง
        int playCount = PlayerPrefs.GetInt(playKey, 0);
        int dPlay = Mathf.Min(playCount, 3);

        playUI.text = $"Play {gameTitle}  3 time ({dPlay}/3)";
        //playUI.text = $"Play Game 3 times ({dPlay}/3)";

        if (playCount >= 3) {
            playUI.color = Color.green;
            playUI.text += " - Done!";
        }

        // 2. ภารกิจได้อันดับหนึ่ง 3 ครั้ง
        int topCount = PlayerPrefs.GetInt(topKey, 0);
        int dTop = Mathf.Min(topCount, 3);

        topUI.text = $"Top score of {gameTitle} 3 time ({dTop}/3)";
        //topUI.text = $"Top Score 3 times ({dTop}/3)";
        
        if (topCount >= 3) {
            topUI.color = Color.green;
            topUI.text += " - SUCCESS!";
        }
    }

    public void GoToHome()
    {
        SceneManager.LoadScene("MainMenu");
    }

        public void GoToStatistic()
    {
        // ไปหน้าสถิติ
        SceneManager.LoadScene("stat_game1"); 
    }
}