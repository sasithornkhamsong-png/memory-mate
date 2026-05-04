using UnityEngine;

public class ProgressData : MonoBehaviour
{
    public static ProgressData instance;

    // ชื่อเกมทั้ง 3
    public static readonly string[] GameNames = { "HappyMarket", "HouseGame", "ProMaid" };

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // ======= บันทึกคะแนนสูงสุด =======
    public void UpdateBestScore(string gameName, int score)
    {
        string key = gameName + "_BestScore";
        if (score > PlayerPrefs.GetInt(key, 0))
        {
            PlayerPrefs.SetInt(key, score);
            PlayerPrefs.Save();
        }
    }

    public int GetBestScore(string gameName)
    {
        return PlayerPrefs.GetInt(gameName + "_BestScore", 0);
    }

    // ======= บันทึกภารกิจ =======
    public void CompleteQuest(string gameName, int questIndex)
    {
        string key = gameName + "_Quest_" + questIndex;
        PlayerPrefs.SetInt(key, 1);
        PlayerPrefs.Save();
    }

    public bool IsQuestComplete(string gameName, int questIndex)
    {
        return PlayerPrefs.GetInt(gameName + "_Quest_" + questIndex, 0) == 1;
    }

    // ======= คำนวณ % ของแต่ละเกม =======
    // maxScore และ totalQuests ต้องกำหนดตามแต่ละเกม
    public float GetProgress(string gameName, int maxScore, int totalQuests)
    {
        // 70% มาจาก high score
        float scorePercent = 0f;
        if (maxScore > 0)
            scorePercent = Mathf.Clamp01((float)GetBestScore(gameName) / maxScore) * 0.7f;

        // 30% มาจากภารกิจ
        float questPercent = 0f;
        if (totalQuests > 0)
        {
            int completed = 0;
            for (int i = 0; i < totalQuests; i++)
                if (IsQuestComplete(gameName, i)) completed++;
            questPercent = Mathf.Clamp01((float)completed / totalQuests) * 0.3f;
        }

        return scorePercent + questPercent; // 0.0 - 1.0
    }
}