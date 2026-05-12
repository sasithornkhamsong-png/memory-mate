using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class ScoreEntry
{
    public int score;
    public float time;
    public string date;
}

[Serializable]
public class ScoreList
{
    public List<ScoreEntry> scores = new List<ScoreEntry>();
}

public static class scoreGame1Manager
{
    private const string PrefKey = "HighScores_Game1";

    public static void SaveEntry(int totalScore, float totalTime)
    {
        // 1. โหลดข้อมูลเดิม
        string json = PlayerPrefs.GetString(PrefKey, "");
        ScoreList scoreList = string.IsNullOrEmpty(json) ? new ScoreList() : JsonUtility.FromJson<ScoreList>(json);

        string currentDate = DateTime.Now.ToString("dd/MM/yyyy HH:mm");
        bool isDuplicate = scoreList.scores.Any(s => s.score == totalScore && s.time == totalTime && s.date == currentDate);    
        if (isDuplicate) 
        {
            Debug.Log("Detected duplicate entry, skipping save.");
            return;
        }

        // 2. เพิ่มข้อมูลใหม่
        ScoreEntry newEntry = new ScoreEntry
        {
            score = totalScore,
            time = totalTime,
            date = DateTime.Now.ToString("dd/MM/yyyy HH:mm")
        };
        scoreList.scores.Add(newEntry);

        // 3. เรียงลำดับตามเงื่อนไข: คะแนนมาก่อนถ้าคะแนนเท่ากันดูที่เวลา
        scoreList.scores = scoreList.scores
            .OrderByDescending(s => s.score)
            .ThenBy(s => s.time)
            .Take(5) // เอาแค่ 5 อันดับแรก
            .ToList();

        // 4. บันทึกกลับลง PlayerPrefs
        string newJson = JsonUtility.ToJson(scoreList);
        PlayerPrefs.SetString(PrefKey, newJson);
        PlayerPrefs.Save();
    }

    public static List<ScoreEntry> GetTopScores()
    {
        string json = PlayerPrefs.GetString(PrefKey, "");
        if (string.IsNullOrEmpty(json)) return new List<ScoreEntry>();
        return JsonUtility.FromJson<ScoreList>(json).scores;
    }
}