using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public QuestItem currentQuest;

    public int stars = 0;

    public TextMeshProUGUI starText;
    public int lastScore;
    public int bestScore;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {       
        bestScore = 90;
        lastScore = 55; // ลองเปลี่ยนค่าเล่น

        FindObjectOfType<CheerUpUI>().UpdateUI();
        //UpdateUI();

    }

    public void AddStar(int amount)
    {
        stars += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        starText.text = stars.ToString();
    }

    
}