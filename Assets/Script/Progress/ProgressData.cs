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

    // ======= บันทึกผลการเล่น =======
    public void SaveGameResult(string gameName, int score, float time)
    {
        // ---------- Last Score ----------
        PlayerPrefs.SetInt(gameName + "_LastScore", score);

        // จำนวนครั้งที่เล่น
        int playCount =
            PlayerPrefs.GetInt(gameName + "_PlayCount", 0);

        PlayerPrefs.SetInt(
            gameName + "_PlayCount",
            playCount + 1
        );

        // ---------- Best Score ----------
        int bestScore = PlayerPrefs.GetInt(gameName + "_BestScore", 0);

        bool isPersonalBest = false;

        if (score > bestScore)
        {
            PlayerPrefs.SetInt(gameName + "_BestScore", score);
            isPersonalBest = true;
        }

        // ---------- Best Time ----------
        float bestTime = PlayerPrefs.GetFloat(gameName + "_BestTime", 0f);

        if (bestTime == 0f || time < bestTime)
        {
            PlayerPrefs.SetFloat(gameName + "_BestTime", time);
        }

        // ---------- Recent 5 Days ----------
        string today =
            System.DateTime.Now.ToString("yyyyMMdd");

        string lastRecentDate =
            PlayerPrefs.GetString(
                gameName + "_LastRecentDate",
                ""
            );

        // เพิ่ม recent แค่ครั้งแรกของวัน
        if (lastRecentDate != today)
        {
            ShiftRecent(gameName);

            if (isPersonalBest)
                PlayerPrefs.SetInt(gameName + "_Recent_0", 2);
            else
                PlayerPrefs.SetInt(gameName + "_Recent_0", 1);

            PlayerPrefs.SetString(
                gameName + "_LastRecentDate",
                today
            );
        }

        /*// ---------- Recent 5 Days ----------
        ShiftRecent(gameName);

        if (isPersonalBest)
            PlayerPrefs.SetInt(gameName + "_Recent_0", 2);
        else
            PlayerPrefs.SetInt(gameName + "_Recent_0", 1);*/

        // ---------- STREAK ----------
        /*string today =
            System.DateTime.Now.ToString("yyyyMMdd");*/

        string lastPlayedDate =
            PlayerPrefs.GetString(
                "Global_LastPlayedDate",
                ""
            );

        // เล่นครั้งแรกของวันเท่านั้น
        if (lastPlayedDate != today)
        {
            StreakController.instance.AddStreak();

            PlayerPrefs.SetString(
                "Global_LastPlayedDate",
                today
            );
        }

        // ---------- Progress ----------
        UpdateProgress(gameName, score, isPersonalBest);

        PlayerPrefs.Save();
    }
        //----------- Recent 5 Days ---------
    void ShiftRecent(string gameName)
    {
        for (int i = 4; i > 0; i--)
        {
            int prev = PlayerPrefs.GetInt(gameName + "_Recent_" + (i - 1), 0);
            PlayerPrefs.SetInt(gameName + "_Recent_" + i, prev);
        }
    }

        //------------ New Progress ----------
    void UpdateProgress(string gameName, int score, bool isPB)
    {
        float progress = PlayerPrefs.GetFloat(gameName + "_Progress", 0f);

        // ===== ความสม่ำเสมอ =====
        progress += 0.02f;

        // ===== คะแนนพัฒนาการ =====
        int lastScore = PlayerPrefs.GetInt(gameName + "_PreviousScore", 0);

        if (score > lastScore)
            progress += 0.03f;

        // ===== Personal Best =====
        if (isPB)
            progress += 0.05f;

        progress = Mathf.Clamp01(progress);

        PlayerPrefs.SetFloat(gameName + "_Progress", progress);

        // save previous
        PlayerPrefs.SetInt(gameName + "_PreviousScore", score);
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
    /*public float GetProgress(string gameName)
    {
        float progress = 0f;

        // ===== ความสม่ำเสมอ 20% ===== ต้องแก้เพิ่ม ===== 
        int playCount =
            PlayerPrefs.GetInt(gameName + "_PlayCount", 0);

        progress += Mathf.Clamp01(playCount / 5f) * 0.2f; // ==== เล่นมากกว่า 5 ครั้งได้ =====
        // ==== เพิ่มจังหวะก่อน reset =====  เผื่อผู้เล่นไม่เห็น success ของตัวเอง

        // ===== คะแนนพัฒนาการ 40% =====
        int bestScore =
            GetBestScore(gameName);

        progress += Mathf.Clamp01(bestScore / 500f) * 0.4f;

        // ===== ความครบถ้วน 40% =====
        int completedQuest = 0;

        for (int i = 0; i < 3; i++)
        {
            if (IsQuestComplete(gameName, i))
                completedQuest++;
        }

        progress += (completedQuest / 3f) * 0.4f;

        return Mathf.Clamp01(progress);
    }*/

    public float GetProgress(string gameName)
    {
        float progress = 0f;

        // ===== ความสม่ำเสมอ 20% =====
        int playDays =
            PlayerPrefs.GetInt(gameName + "_PlayDays", 0);

        progress += Mathf.Clamp01((playDays - 1) / 5f) * 0.2f;

        // ===== คะแนนพัฒนาการ 40% =====
        int bestScore =
            GetBestScore(gameName);

        progress += Mathf.Clamp01(bestScore / 500f) * 0.4f;

        // ===== ความครบถ้วน 40% =====
        int completedQuest = 0;

        for (int i = 0; i < 3; i++)
        {
            if (IsQuestComplete(gameName, i))
                completedQuest++;
        }

        progress += (completedQuest / 3f) * 0.4f;

        return Mathf.Clamp01(progress);
    }

    void SaveRecent(string gameName, int score)
    {
        int bestScore =
            PlayerPrefs.GetInt(gameName + "_BestScore", 0);

        int resultType = 1;

        // ทำลายสถิติ
        if (score >= bestScore && bestScore > 0)
        {
            resultType = 2;
        }

        // เลื่อนข้อมูลเก่า
        for (int i = 4; i > 0; i--)
        {
            int oldData =
                PlayerPrefs.GetInt(gameName + "_Recent_" + (i - 1), 0);

            PlayerPrefs.SetInt(
                gameName + "_Recent_" + i,
                oldData
            );
        }

        // ใส่อันใหม่
        PlayerPrefs.SetInt(
            gameName + "_Recent_0",
            resultType
        );
    }
}