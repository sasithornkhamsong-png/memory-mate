using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressCircleUI : MonoBehaviour
{
    public Image circle;
    public TextMeshProUGUI percentText;

    [Header("ตั้งค่าแต่ละเกม")]
    public string gameName;    // "HappyMarket" / "HouseGame" / "ProMaid"
    public int maxScore;       // คะแนนเต็มของเกมนี้
    public int totalQuests;    // จำนวนภารกิจทั้งหมด

    public void SetProgress()
    {
        float progress = ProgressData.instance.GetProgress(gameName, maxScore, totalQuests);
        circle.fillAmount = progress;
        percentText.text = Mathf.RoundToInt(progress * 100) + "%";
    }

    void Start()
    {
        SetProgress();
    }
}