using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CheerUpUI : MonoBehaviour 
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI subText;
    public Image icon;

    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI bestTimeText;

    public Sprite bestSprite;  
    public Sprite normalSprite; 
    public Sprite badSprite;    

    void OnEnable()
    {
        UpdateUI();
    }

    public void UpdateUI() 
    {
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager is null!");
            return;
        }

        int lastScore = GameManager.instance.lastScore;
        int bestScore = GameManager.instance.bestScore;

        bestScoreText.text = bestScore.ToString();
        
        float time = GameManager.instance.bestTime;
        if (time > 0)
            bestTimeText.text = time.ToString("F1") + "s";
        else
            bestTimeText.text = "--";

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