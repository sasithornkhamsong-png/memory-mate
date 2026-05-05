using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MissionManager : MonoBehaviour
{
    [Header("ช่อง UI แสดงข้อความภารกิจ")]
    public TextMeshProUGUI playMissionText;      // ช่องสำหรับ "เล่นเกมครบ 3 ครั้ง"
    public TextMeshProUGUI topScoreMissionText;  // ช่องสำหรับ "ได้อันดับหนึ่ง 3 ครั้ง"

    void Start()
    {
        CheckMissions();
    }

    void CheckMissions()
    {
        // 1. ดึงข้อมูลที่บันทึกไว้
        int playCount = PlayerPrefs.GetInt("Mission_PlayCount", 0);
        int topScoreCount = PlayerPrefs.GetInt("Mission_TopScoreCount", 0);

        // 2. จัดการภารกิจ "เล่นเกมครบ 3 ครั้ง"
        // ใช้ Mathf.Min เพื่อไม่ให้ตัวเลขโชว์เกิน 3 (เช่น ถ้าเล่นไป 5 รอบ ก็จะโชว์แค่ 3/3)
        int displayPlay = Mathf.Min(playCount, 3); 
        playMissionText.text = $"Play game 3 time  ( {displayPlay} / 3 )";
        
        if (playCount >= 3)
        {
            playMissionText.color = Color.green; // เปลี่ยนสีตัวอักษรเป็นสีเขียวเมื่อสำเร็จ
            playMissionText.text += " - Done!";
        }

        // 3. จัดการภารกิจ "สร้างสถิติอันดับหนึ่ง 3 ครั้ง"
        int displayTopScore = Mathf.Min(topScoreCount, 3);
        topScoreMissionText.text = $"Make new high score 3 time ( {displayTopScore} / 3 )";
        
        if (topScoreCount >= 3)
        {
            topScoreMissionText.color = Color.green;
            topScoreMissionText.text += " - SUCCESS!";
        }
    }

    public void GoToHome()
    {
        SceneManager.LoadScene("Scene_Home");
    }

        public void PlayAgain()
    {
        // กลับไปด่าน 1
        SceneManager.LoadScene("Scene_Level1"); 
    }

        public void GoToStatistic()
    {
        // ไปหน้าสถิติ
        SceneManager.LoadScene("Scene_Statistic"); 
    }
}
