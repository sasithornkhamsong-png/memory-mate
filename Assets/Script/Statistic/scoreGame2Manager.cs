using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Score2Entry
{
    public int score;
    public float time;
    public string date;
}

[Serializable]
public class Score2List
{
    public List<Score2Entry> scores = new List<Score2Entry>();
}

public static class scoreGame2Manager
{
    private const string PrefKey = "HighScores_Game2";

    public static void SaveEntry(int FinalScore, float FinalTime)
    {
        // 1. โหลดข้อมูลเดิม
        string json = PlayerPrefs.GetString(PrefKey, "");
        Score2List score2List = string.IsNullOrEmpty(json) ? new Score2List() : JsonUtility.FromJson<Score2List>(json);

        // --- เริ่มต้น: ระบบตรวจสอบภารกิจทำลายสถิติอันดับ 1 ---
        if (score2List.scores.Count > 0)
        {
            Score2Entry currentTop = score2List.scores[0]; 
            bool isNewRecord = false;

            if (FinalScore > currentTop.score) {
                isNewRecord = true;
            } else if (FinalScore == currentTop.score && FinalTime < currentTop.time) {
                isNewRecord = true;
            }

            if (isNewRecord) {
                int breakRecordCount = PlayerPrefs.GetInt("Game2_BreakTopScoreQuest", 0);
                PlayerPrefs.SetInt("Game2_BreakTopScoreQuest", breakRecordCount + 1);
            }
        }
        // --- สิ้นสุด: ระบบตรวจสอบภารกิจ ---
        
        
        // 2. เพิ่มข้อมูลใหม่
        Score2Entry newEntry = new Score2Entry
        {
            score = FinalScore,
            time = FinalTime,
            date = DateTime.Now.ToString("dd/MM/yyyy HH:mm")
        };
        score2List.scores.Add(newEntry);

        // 3. เรียงลำดับตามเงื่อนไข: คะแนนมาก่อน (Descending) ถ้าเท่ากันดูที่เวลา (Ascending)
        score2List.scores = score2List.scores
            .OrderByDescending(s => s.score)
            .ThenBy(s => s.time)
            .Take(5) // เอาแค่ 5 อันดับแรก
            .ToList();

        // 4. บันทึกกลับลง PlayerPrefs
        string newJson = JsonUtility.ToJson(score2List);
        PlayerPrefs.SetString(PrefKey, newJson);
        PlayerPrefs.Save();
    }

    public static List<Score2Entry> GetTopScores()
    {
        string json = PlayerPrefs.GetString(PrefKey, "");
        if (string.IsNullOrEmpty(json)) return new List<Score2Entry>();
        return JsonUtility.FromJson<Score2List>(json).scores;
    }
}
