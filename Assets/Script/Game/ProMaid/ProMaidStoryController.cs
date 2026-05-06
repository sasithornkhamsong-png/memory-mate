using UnityEngine;
using TMPro;

public class ProMaidStoryController : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI storyText;
    public GameObject panelStory;

    [Header("Panels")]
    //public GameObject panelShelfGame;
    public GameObject panelPartyGame;
   //public GameObject panelMatchingGame;
    public GameObject panelScore; // สร้างทีหลังได้ ใส่ null ไปก่อน

    private string[][] allStories;
    private int currentStorySet = 0;
    private int currentIndex = 0;

    void Start()
    {
        allStories = new string[][]
        {
            // ก่อน ShelfGame
            new string[]
            {
                "ยินดีต้อนรับสู่บ้าน ProMaid...",
                "คุณมีภารกิจต้องจัดของบนชั้น..."
            },
            // ก่อน PartyGame
            new string[]
            {
                "คุณจัดของได้สำเร็จ!",
                "ตอนนี้มีแขกมาร่วมงานเลี้ยง..."
            },
            // หลังจบทุกด่าน
            new string[]
            {
                "คุณทำภารกิจสำเร็จทุกอย่างแล้ว!"
            }
        };

        ShowStory();
    }

    void ShowStory()
    {
        storyText.text = allStories[currentStorySet][currentIndex];
    }

    public void NextStory()
    {
        currentIndex++;

        if (currentIndex < allStories[currentStorySet].Length)
        {
            ShowStory();
        }
        else
        {
            GoToNextGame();
        }
    }

    void GoToNextGame()
    {
        panelStory.SetActive(false);

        panelPartyGame.SetActive(true);
    }

    public void StartNextStory()
    {
        currentStorySet++;
        currentIndex = 0;

        panelStory.SetActive(true);
        ShowStory();
    }
}