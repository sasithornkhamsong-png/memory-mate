using UnityEngine;
using TMPro;

public class StatsUI : MonoBehaviour
{
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI bestTimeText;

    public string gameName; // ใส่ใน Inspector เช่น "HouseGame"

    void Start()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        int score = PlayerPrefs.GetInt(gameName + "_BestScore", 0);
        float time = PlayerPrefs.GetFloat(gameName + "_BestTime", 0f);

        bestScoreText.text = score.ToString();
        bestTimeText.text = time > 0
            ? time.ToString("F1") + "s"
            : "--";
    }
}