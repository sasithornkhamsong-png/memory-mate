using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour
{
    [Header("Star System UI")]
    public TextMeshProUGUI starCountText;
    
    [Header("Game Names")]
    public string game1Name = "บ้านปิดตาย";
    public string game2Name = "แม่บ้านมืออาชีพ";
    public string game3Name = "แฮปปี้มาร์เก็ต";

    [Header(" Game1_m1 : เล่นครบ 3 ครั้ง")]
    public TextMeshProUGUI m1DescText;      // ช่องสำหรับคำอธิบาย
    public TextMeshProUGUI m1ProgressText;  // ช่องสำหรับตัวเลข (เช่น 0/3)

    [Header("Game2_m1: เล่นครบ 3 ครั้ง")]
    public TextMeshProUGUI m2DescText;
    public TextMeshProUGUI m2ProgressText;

    [Header("Game3_m1: เล่นครบ 3 ครั้ง")]
    public TextMeshProUGUI m3DescText;
    public TextMeshProUGUI m3ProgressText;

    [Header("Game1_m2: คะแนน > 290 (3 ครั้ง)")]
    public TextMeshProUGUI m4DescText;
    public TextMeshProUGUI m4ProgressText;

    [Header("Game2_m2: คะแนน > 290 (3 ครั้ง)")]
    public TextMeshProUGUI m5DescText;
    public TextMeshProUGUI m5ProgressText;

    [Header("Game3_m2: คะแนน > 290 (3 ครั้ง)")]
    public TextMeshProUGUI m6DescText;
    public TextMeshProUGUI m6ProgressText;

    [Header("Game1_m3: ทำลายสถิติที่ 1")]
    public TextMeshProUGUI m7DescText;
    public TextMeshProUGUI m7ProgressText;

    [Header("Game2_m3: ทำลายสถิติที่ 1 ")]
    public TextMeshProUGUI m8DescText;
    public TextMeshProUGUI m8ProgressText;

    [Header("Game3_m3: ทำลายสถิติที่ 1")]
    public TextMeshProUGUI m9DescText;
    public TextMeshProUGUI m9ProgressText;

    void Start()
    {
        UpdateAllMissions();
        UpdateStarUI();
    }

    void UpdateAllMissions()
    {
        // --- ภารกิจเล่นเกมครบ 3 ครั้ง ---
        DisplayMission(1, $"เล่นเกม{game1Name} 3 ครั้ง", "Game1_PlayCount", 3, m1DescText, m1ProgressText);
        DisplayMission(2, $"เล่นเกม{game2Name} 3 ครั้ง", "Game2_PlayCount", 3, m2DescText, m2ProgressText);
        DisplayMission(3, $"เล่นเกม{game3Name} 3 ครั้ง", "Game3_PlayCount", 3, m3DescText, m3ProgressText);

        // --- ภารกิจทำคะแนนมากกว่า 290 คะแนน จำนวน 3 ครั้ง ---
        DisplayMission(4, $"ทำคะแนนเกม{game1Name} > 290", "Game1_HighScoreQuest", 3, m4DescText, m4ProgressText);
        DisplayMission(5, $"ทำคะแนนเกม{game2Name} > 290", "Game2_HighScoreQuest", 3, m5DescText, m5ProgressText);
        DisplayMission(6, $"ทำคะแนนเกม{game3Name} > 290", "Game3_HighScoreQuest", 3, m6DescText, m6ProgressText);

        // --- ภารกิจทำลายสถิติอันดับ 1 เดิม ---
        DisplayMission(7, $"ทำลายสถิติเกม{game1Name}", "Game1_BreakTopScoreQuest", 1, m7DescText, m7ProgressText);
        DisplayMission(8, $"ทำลายสถิติเกม{game2Name}", "Game2_BreakTopScoreQuest", 1, m8DescText, m8ProgressText);
        DisplayMission(9, $"ทำลายสถิติเกม{game3Name}", "Game3_BreakTopScoreQuest", 1, m9DescText, m9ProgressText);
    }

    void DisplayMission(int missionID, string description, string prefsKey, int targetCount, TextMeshProUGUI descUI, TextMeshProUGUI progressUI)
    {
        if (descUI == null || progressUI == null) return;

        int currentCount = PlayerPrefs.GetInt(prefsKey, 0);
        int displayCount = Mathf.Min(currentCount, targetCount); 

        // 1. ใส่ข้อความคำอธิบาย
        descUI.text = description;
        
        // 2. ใส่ข้อความความคืบหน้า (เช่น 1/3)
        progressUI.text = $"{displayCount}/{targetCount}";

        // 3. ตรวจสอบความสำเร็จ
        if (currentCount >= targetCount)
        {
            // เปลี่ยนสีเป็นสีเขียวทั้งสองช่องเมื่อทำสำเร็จ
            descUI.color = Color.green;
            progressUI.color = Color.green;
            
            // เปลี่ยนตัวเลขเป็นคำว่าสำเร็จ (หรือจะคง 3/3 ไว้ก็ได้ตามต้องการ)
            progressUI.text = "DONE!";

            AwardStar(missionID); 
        }
        else
        {
            // เปลี่ยนสีกลับเป็นสีขาว (หรือสีตั้งต้น) เมื่อยังไม่สำเร็จ
            descUI.color = Color.black; 
            progressUI.color = Color.black; 
        }
    }

    void AwardStar(int missionID)
    {
        string starKey = "MissionStar_" + missionID; // เช่น MissionStar_1
        
        // เช็คว่าภารกิจนี้เคยได้ดาวไปหรือยัง (0 = ยังไม่เคย, 1 = ได้แล้ว)
        if (PlayerPrefs.GetInt(starKey, 0) == 0)
        {
            // เพิ่มดาวรวม
            int currentTotalStars = PlayerPrefs.GetInt("TotalStars", 0);
            PlayerPrefs.SetInt("TotalStars", currentTotalStars + 1);
            
            // ทำเครื่องหมายว่าภารกิจนี้ได้ดาวไปแล้ว
            PlayerPrefs.SetInt(starKey, 1);
            PlayerPrefs.Save();

            UpdateStarUI(); // อัปเดตการแสดงผลทันที
            Debug.Log("Mission " + missionID + " Completed! Star Awarded.");
        }
    }

    void UpdateStarUI()
    {
        if (starCountText != null)
        {
            int total = PlayerPrefs.GetInt("TotalStars", 0);
            starCountText.text = total.ToString(); // แสดงจำนวนดาวรวม
        }
    }


    public void GoToHome()
    {
        SceneManager.LoadScene("MainMenu"); 
    }

        public void GoToRank5()
    {
        SceneManager.LoadScene("stat_game1"); 
    }
}