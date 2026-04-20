using UnityEngine;

public class ProgressData : MonoBehaviour
{
    public static ProgressData instance;

    public int totalPlay = 0;
    public int totalWin = 0;

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

        ProgressData.instance.totalPlay++;
        ProgressData.instance.totalWin++;
    }

    public void LoseGame()
    {
        Debug.Log("You lose!");

        ProgressData.instance.totalPlay++;
    }
}