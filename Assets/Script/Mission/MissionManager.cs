using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour
{
    [Header("Game Names")]
    public string game1Name = "HouseGame";
    public string game2Name = "ProMaid";
    public string game3Name = "HappyMarket";

    [Header("Mission UI: Play Count (เล่นครบ 3 ครั้ง)")]
    public TextMeshProUGUI mission1Text; // ภารกิจที่ 1: เล่น HouseGame
    public TextMeshProUGUI mission2Text; // ภารกิจที่ 2: เล่น ProMaid
    public TextMeshProUGUI mission3Text; // ภารกิจที่ 3: เล่น HappyMarket

    [Header("Mission UI: High Score (คะแนน > 290 ครบ 3 ครั้ง)")]
    public TextMeshProUGUI mission4Text; // ภารกิจที่ 4: HouseGame > 290
    public TextMeshProUGUI mission5Text; // ภารกิจที่ 5: ProMaid > 290
    public TextMeshProUGUI mission6Text; // ภารกิจที่ 6: HappyMarket > 290

    void Start()
    {
        UpdateAllMissions();
    }

    void UpdateAllMissions()
    {
        // --- ภารกิจ 1-3: เล่นเกมครบ 3 ครั้ง ---
        DisplayMission($"Play {game1Name} ", "Game1_PlayCount", 3, mission1Text);
        DisplayMission($"Play {game2Name} ", "Game2_PlayCount", 3, mission2Text);
        DisplayMission($"Play {game3Name} ", "Game3_PlayCount", 3, mission3Text);

        // --- ภารกิจ 4-6: ทำคะแนนมากกว่า 290 คะแนน จำนวน 3 ครั้ง ---
        DisplayMission($"Make {game1Name} Score > 290", "Game1_HighScoreQuest", 3, mission4Text);
        DisplayMission($"Make {game2Name} Score > 290", "Game2_HighScoreQuest", 3, mission5Text);
        DisplayMission($"Make {game3Name} Score > 290", "Game3_HighScoreQuest", 3, mission6Text);
    }

    // ฟังก์ชันสำหรับจัดรูปแบบข้อความและตรวจสอบความสำเร็จ
    void DisplayMission(string description, string prefsKey, int targetCount, TextMeshProUGUI uiText)
    {
        if (uiText == null) return;

        int currentCount = PlayerPrefs.GetInt(prefsKey, 0);
        int displayCount = Mathf.Min(currentCount, targetCount); // ป้องกันตัวเลขเกินเป้าหมาย (เช่น 4/3 ให้เป็น 3/3)

        uiText.text = $"{description} ({displayCount}/{targetCount})";

        if (currentCount >= targetCount)
        {
            uiText.color = Color.green;
            uiText.text += " - Done!";
        }
        else
        {
            uiText.color = Color.white; // สีตั้งต้นเมื่อยังไม่สำเร็จ
        }
    }

    // ปุ่มกลับหน้าหลัก
    public void GoToHome()
    {
        SceneManager.LoadScene("MainMenu"); // เปลี่ยนชื่อเป็น Scene หน้า Home ของคุณ
    }
}