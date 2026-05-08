using UnityEngine;
using TMPro;

public class HappyMarketStoryController : MonoBehaviour
{
    [Header("UI")]
    public TextMeshProUGUI storyText;
    public GameObject panelStory;

    [Header("Panels")]
    public GameObject panelCheckListGame;
    public GameObject panelShoppingGame;
    public GameObject panelSortingGame;
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
                "ยินดีต้นรับ สู่ด่าน Happy Market",
                "คุณมีภารกิจในวันนี้ คือ..."
            },
            // ก่อน PartyGame
            new string[]
            {
                "คุณทำภารกิจได้สำเร็จ!",
                "ตอนนี้ได้เวลาของภารกิจต่อไป..."
            },
            new string[]   // ← เพิ่มใหม่ ก่อน SortingGame
            {
                "เกือบถึงแล้ว!",
                "ภารกิจสุดท้ายรอคุณอยู่..."
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

        if (currentStorySet == 0)
        {
            panelCheckListGame.SetActive(true);

            HappyMarketGame game =
            panelCheckListGame.GetComponent<HappyMarketGame>();

        game.StartChecklistGame();
        }
        else if (currentStorySet == 1)
        {
            panelShoppingGame.SetActive(true);
        }
        else if (currentStorySet == 2)
        {
            panelSortingGame.SetActive(true); // ← เพิ่ม
        }    
        
        else if (currentStorySet == 3)
        {
            // จบเกมทั้งหมด → แสดงคะแนน
            if (panelScore != null)
                panelScore.SetActive(true);
            else
                Debug.Log("TODO: แสดงหน้าคะแนน");
        }

         currentStorySet++;
    }

    public void StartNextStory()
    {
        currentStorySet++;
        currentIndex = 0;

        panelStory.SetActive(true);
        ShowStory();
    }
}