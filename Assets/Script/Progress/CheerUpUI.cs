using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheerUpUI : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI subText;
    public Image icon;

    public Sprite bestSprite;
    public Sprite normalSprite;
    public Sprite badSprite;

    public string gameName; // ใส่ใน Inspector

    void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        int bestScore = PlayerPrefs.GetInt(gameName + "_BestScore", 0);
        int lastScore = PlayerPrefs.GetInt(gameName + "_LastScore", 0);

        if (lastScore >= bestScore && bestScore != 0)
        {
            titleText.text = "วันนี้เก่งมาก!";
            subText.text = "ทำลายสถิติได้แล้ว";
            icon.sprite = bestSprite;
        }
        else if (lastScore > 50)
        {
            titleText.text = "วันนี้ก็ทำได้ดีอีกแล้ว!";
            subText.text = "เอาอีก อย่าหยุดนะ";
            icon.sprite = normalSprite;
        }
        else
        {
            titleText.text = "อย่าเพิ่งยอมแพ้!";
            subText.text = "ต้องเก่งได้มากกว่านี้แน่ ๆ";
            icon.sprite = badSprite;
        }
    }
}