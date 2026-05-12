using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Score3Entry
{
    public int score;
    public float time;
    public string date;
}

[Serializable]
public class Score3List
{
    public List<Score3Entry> scores = new List<Score3Entry>();
}

public static class scoreGame3Manager
{
    private const string PrefKey = "HighScores_Game2";

    public static void SaveEntry(int FinalScore, float FinalTime)
    {
        // 1. โหลดข้อมูลเดิม
        string json = PlayerPrefs.GetString(PrefKey, "");
        Score3List score3List = string.IsNullOrEmpty(json) ? new Score3List() : JsonUtility.FromJson<Score3List>(json);

        // 2. เพิ่มข้อมูลใหม่
        Score3Entry newEntry = new Score3Entry
        {
            score = FinalScore,
            time = FinalTime,
            date = DateTime.Now.ToString("dd/MM/yyyy HH:mm")
        };
        score3List.scores.Add(newEntry);

        // 3. เรียงลำดับตามเงื่อนไข: คะแนนมาก่อน (Descending) ถ้าเท่ากันดูที่เวลา (Ascending)
        score3List.scores = score3List.scores
            .OrderByDescending(s => s.score)
            .ThenBy(s => s.time)
            .Take(5) // เอาแค่ 5 อันดับแรก
            .ToList();

        // 4. บันทึกกลับลง PlayerPrefs
        string newJson = JsonUtility.ToJson(score3List);
        PlayerPrefs.SetString(PrefKey, newJson);
        PlayerPrefs.Save();
    }

    public static List<Score3Entry> GetTopScores()
    {
        string json = PlayerPrefs.GetString(PrefKey, "");
        if (string.IsNullOrEmpty(json)) return new List<Score3Entry>();
        return JsonUtility.FromJson<Score3List>(json).scores;
    }
}
