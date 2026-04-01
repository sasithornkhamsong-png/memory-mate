using UnityEngine;
using TMPro;

public class StoryController : MonoBehaviour
{
    public TextMeshProUGUI storyText;

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
                "บ้านหลังนี้ถูกปิดตาย...",
                "คุณได้รับภารกิจ..."
            },
            new string[]
            {
                "คุณผ่านด่านแรกมาได้",
                "แต่ยังมีบางอย่างผิดปกติ..."
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