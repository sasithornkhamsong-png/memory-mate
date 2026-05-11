using UnityEngine;
using TMPro;

public class StoryController : MonoBehaviour
{
    public TextMeshProUGUI storyText;

    public BackgroundManager bgManager;

    public GameObject panelStory;
    public GameObject panelPasscode;
    public GameObject panelTableGame; // เพิ่ม

    private string[][] allStories;
    private int currentStorySet = 0;
    private int currentIndex = 0;

    void Start()
    {
        allStories = new string[][]
        {
            new string[]
            {
                "บ้านหลังนี้ถูกปิดตายเนื่องจากตำนานลี้ลับ\nคุณจะต้องทำการจัดการกับภารกิจต่อไปนี้",
                "เพื่อความปลอดภัย คุณจะได้รับรหัสประตูบ้านที่ต้องใช้ตวามจำเท่านั้น",
                "รหัสที่จะแสดงต่อไปนี้ จงจำให้ถูกต้องด้วยล่ะ !"
            },
            new string[]
            {
                "แย่ล่ะ จะขึ้นไปด้านบน แต่ดันมีโต๊ะมาขวางซะได้",
                "แต่เอ๊ะ นั่นมันอะไร ตัวเลขเปลี่ยนไปงั้นหรอ",
                "คุณจะต้องกำจัดตัวเลขที่ไม่ถูกต้องออกไปเสียก่อน"
            },
            new string[]
            {
                "คุณเริ่มเข้าใกล้ความจริง..."
            }
        };

        ShowStory();
    }

    void ShowStory()
    {
        storyText.text = allStories[currentStorySet][currentIndex];

        if (currentStorySet == 0)
        {
            bgManager.SetBG(bgManager.storyBG);
        }
        else if (currentStorySet == 1)
        {
            bgManager.SetBG(bgManager.marketBG);
        }
    }
        
    /*void ShowStory()
    {
        storyText.text = allStories[currentStorySet][currentIndex];
    }*/

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
            panelPasscode.SetActive(true);
        }
        else if (currentStorySet == 1)
        {
            panelTableGame.SetActive(true);
        }
    }

    public void StartNextStory()
    {
        currentStorySet++;
        currentIndex = 0;

        panelStory.SetActive(true);
        ShowStory();
    }
}