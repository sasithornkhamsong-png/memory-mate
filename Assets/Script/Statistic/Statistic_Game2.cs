using UnityEngine;
using TMPro; // สำหรับ TextMeshPro
using UnityEngine.SceneManagement; // สำหรับทำปุ่มกลับหน้าหลัก

public class StatisticManager : MonoBehaviour
{
    [Header("ช่อง UI สำหรับแสดงผลทั้ง 5 อันดับ")]
    // ใช้ Array เพื่อเก็บ TextMeshPro 5 ตัว สำหรับแสดงผลอันดับ 1-5
    public TextMeshProUGUI[] rankTexts; 

    void Start()
    {
        // โหลดและแสดงผลสถิติทันทีที่เปิดหน้านี้ขึ้นมา
        LoadAndDisplayStatistics();
    }

    void LoadAndDisplayStatistics()
    {
        // 1. เคลียร์ข้อความเก่าออกก่อน (อันดับไหนยังไม่มีข้อมูล จะโชว์เป็นขีด "-")
        for (int i = 0; i < rankTexts.Length; i++)
        {
            rankTexts[i].text = "No. " + (i + 1) + " : - No Data -";
        }

        // 2. โหลดข้อมูล JSON จาก PlayerPrefs ที่เซฟไว้จากด่าน CardManager
        string json = PlayerPrefs.GetString("Leaderboard", "");

        // ถ้าไม่มีข้อมูลเลย ให้จบฟังก์ชันแค่นี้
        if (string.IsNullOrEmpty(json))
        {
            return; 
        }

        // 3. แปลงข้อความ JSON กลับมาเป็นคลาส ScoreData
        // (คลาส ScoreData และ ScoreRecord สามารถเรียกใช้ได้เลย เพราะเราสร้างไว้นอกคลาส CardManager แล้ว)
        ScoreData data = JsonUtility.FromJson<ScoreData>(json);

        // 4. นำข้อมูลมาแสดงผลใน TextMeshPro ตามจำนวนที่มีในเซฟ
        for (int i = 0; i < data.records.Count; i++)
        {
            // ป้องกัน Error กรณีที่จำนวนข้อมูลมีมากกว่าช่อง UI ที่เตรียมไว้
            if (i >= rankTexts.Length) break; 

            int score = data.records[i].score;
            float time = data.records[i].time;
            string date = data.records[i].date;

            // แปลงเวลาเป็น นาทีและวินาที
            int totalSeconds = Mathf.FloorToInt(time);
            int minutes = totalSeconds / 60;
            int seconds = totalSeconds % 60;
            
            string timeDisplay = "";
            if (minutes > 0)
                timeDisplay = minutes + " m " + seconds + " s";
            else
                timeDisplay = seconds + " s";

            // 5. นำข้อมูลไปใส่ในช่อง UI ของอันดับนั้นๆ
            rankTexts[i].text = $"No. {i + 1} | score: {score} | time: {timeDisplay} | date: {date}";
        }
    }

    // ฟังก์ชันสำหรับผูกกับปุ่มย้อนกลับ
    public void GoToHome()
    {
        SceneManager.LoadScene("Scene_Home"); 
    }
}

