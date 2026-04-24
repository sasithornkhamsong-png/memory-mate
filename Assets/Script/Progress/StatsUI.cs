using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI bestTimeText;

    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        bestScoreText.text = ProgressData.instance.bestScore.ToString();

        float time = ProgressData.instance.bestTime;
        bestTimeText.text = time > 0
            ? time.ToString("F1") + "s"
            : "--";
    }
}