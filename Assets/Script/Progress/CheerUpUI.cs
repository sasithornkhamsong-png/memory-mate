using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheerUpUI : MonoBehaviour 
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI subText;
    public Image icon;

    public Sprite bestSprite;   // 🎉
    public Sprite normalSprite; // 🙂
    public Sprite badSprite;    // 😢

    void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI() 
    {
        int lastScore = GameManager.instance.lastScore;
        int bestScore = GameManager.instance.bestScore;

        Debug.Log("CheerUpUI RUN"); // ⭐
        Debug.Log("last=" + lastScore + " best=" + bestScore); // ⭐

        // ⭐ ทำลายสถิติ
        if (lastScore >= bestScore && bestScore != 0)
        {
            titleText.text = "วันนี้เก่งมาก!";
            subText.text = "ทำลายสถิติได้แล้ว 🎉";
            icon.sprite = bestSprite;
        }
        // 🙂 ปกติ
        else if (lastScore > 50)
        {
            titleText.text = "วันนี้ก็ทำได้ดีอีกแล้ว!";
            subText.text = "เอาอีก อย่าหยุดนะ 💪 ";
            icon.sprite = normalSprite;
        }
        // 😢 ยังไม่ได้เล่น
        else
        {
            titleText.text = "อย่าเพิ่งยอมแพ้!";
            subText.text = "ต้องเก่งได้มากกว่านี้แน่ ๆ";
            icon.sprite = badSprite;
        }
    }
}