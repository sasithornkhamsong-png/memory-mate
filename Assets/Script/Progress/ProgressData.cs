using UnityEngine;

public class ProgressData : MonoBehaviour
{
    public static ProgressData instance;

    public int totalPlay = 0;
    public int totalWin = 0;

    public int bestScore = 0;
    public float bestTime = 0f;

    void Awake()
    {
        if (instance == null)
            instance = this;
    }

    public float GetProgress()
    {
        if (totalPlay == 0) return 0;
        return (float)totalWin / totalPlay;
    }

    public void WinGame()
    {
        Debug.Log("You win!");

        totalPlay++;
        totalWin++;
    }

    public void LoseGame()
    {
        Debug.Log("You lose!");

        totalPlay++;
    }

    public void UpdateBestScore(int score)
    {
        if (score > bestScore)
        {
            bestScore = score;
        }
    }

    public void UpdateBestTime(float time)
    {
        if (bestTime == 0 || time < bestTime)
        {
            bestTime = time;
        }
    }
}