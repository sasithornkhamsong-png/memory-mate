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
    public GameObject panelScore; 

    [Header("Game Scripts")]
    public HappyMarketGame happyMarketGame;

    private string[][] allStories;
    private int currentStorySet = 0;
    private int currentIndex = 0;

    void Start()
    {
        allStories = new string[][]
        {
            new string[]
            {
                "วันนี้มีตลาดเปิดใหม่ใกล้ ๆ หมู่บ้าน\nต้องแวะไปหาซื้อของเข้าบ้านสักหน่อยนะ",
                "แต่แย่จัง กระดาษที่พกมาด้วยดันเปียกน้ำ\nต้องอาศัยความจำอันดีซะแล้ว\nว่าจดอะไรมาบ้าง"
            },
            new string[]
            {
                "สำเร็จ ได้รายการที่จะต้องซื้อแล้ว!",
                "อืม..\nแต่จำได้ว่าห้างตรงหัวมุม กำลังจัดช่วงลดราคา\nจะพลาดไม่ได้"
            },
            new string[]   // ← เพิ่มใหม่ ก่อน SortingGame
            {
                "ซื้อของกลับมาแล้ว ใกล้ได้เวลาอาหารแล้วสินะ",
                "มาเริ่มทำมื้อแรกกันเลยดีกว่า"
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
            happyMarketGame.StartChecklistGame(); // เรียกตรงๆ
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

         //currentStorySet++;
    }

    public void StartNextStory()
    {
        Debug.Log("StartNextStory called! currentStorySet: " + currentStorySet);
        currentStorySet++;
        currentIndex = 0;

        Debug.Log("panelStory is null: " + (panelStory == null));
        Debug.Log("panelStory activeSelf before: " + panelStory.activeSelf);

        panelStory.SetActive(true);
        Debug.Log("panelStory activeSelf after: " + panelStory.activeSelf);

        ShowStory();
    }
}