using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class HappyMarketGame : MonoBehaviour
{
    [Header("Stage 1 - Memory Game")]
    public GameObject panelMemory;
    public GameObject panelSelect;
    public TextMeshProUGUI memoryText;
    public TextMeshProUGUI resultText;
    public Transform gridParent;
    public GameObject itemPrefab;
    private float timeElapsed = 0f;
    private bool isTimerRunning = true;

    [Header("Stage 2 - Shopping Game")]
    public GameObject panelMemorize;
    public GameObject panelBudget;
    public GameObject panelShopping;
    public HappyMarketCardUI currentCard;
    public Button nextButton;
    public TextMeshProUGUI cardCounterText;
    public TextMeshProUGUI budgetText;

    [Header("Data")]
    public List<string> allItems = new();
    public List<string> targetItems = new();
    public List<MarketItem> allMarketItems = new();

    private List<string> selectedItems = new();
    private int correctCount = 0;

    [Header("Score")]
    public TextMeshProUGUI scoreText;
    private int totalScore = 100;
    private int penaltyScore = 0;

    private readonly List<MarketItem> shoppingTargetItems = new();
    private int currentCardIndex = 0;
    private int playerBudget = 0;

    private void Start()
    {
        StartChecklistGame();

        totalScore = 100;
        scoreText.text = "Score : " + totalScore;
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timeElapsed += Time.deltaTime;
        }
    }

    public void StartChecklistGame()
    {
        SetupGame();
    }

    // =========================
    // STAGE 1: MEMORY GAME
    // =========================
    void SetupGame()
    {
        panelMemory.SetActive(true);
        panelSelect.SetActive(false);

        if (panelMemorize != null) panelMemorize.SetActive(false);
        if (panelBudget != null) panelBudget.SetActive(false);
        if (panelShopping != null) panelShopping.SetActive(false);

        GenerateTargetItems(3);
        ShowMemoryList();
    }

    void GenerateTargetItems(int count)
    {
        targetItems.Clear();

        List<string> temp = new(allItems);
        count = Mathf.Min(count, temp.Count);

        for (int i = 0; i < count; i++)
        {
            int rand = Random.Range(0, temp.Count);
            targetItems.Add(temp[rand]);
            temp.RemoveAt(rand);
        }
    }

    void ShowMemoryList()
    {
        memoryText.text = "";

        foreach (string item in targetItems)
        {
            memoryText.text += "• " + item + "\n";
        }
    }

    public void OnStartSelect()
    {
        if (targetItems.Count == 0)
        {
            GenerateTargetItems(3);
        }

        panelMemory.SetActive(false);
        panelSelect.SetActive(true);

        ResetSelection();
        GenerateSelectItems();
    }

    void ResetSelection()
    {
        selectedItems.Clear();
        correctCount = 0;

        resultText.text = "";
    }

    public void OnItemClicked(string item, TextMeshProUGUI textUI)
    {
        if (selectedItems.Contains(item)) return;

        selectedItems.Add(item);

        // เพิ่ม .Trim().ToLower() กันพลาด
        bool isCorrect = targetItems.Exists(t => t.Trim().ToLower() == item.Trim().ToLower());

        if (isCorrect)
        {
            textUI.color = Color.green;
            correctCount++;

            if (correctCount >= targetItems.Count)
            {
                isTimerRunning = false;

                PlayerPrefs.SetInt("HappyMarketScore", totalScore);
                PlayerPrefs.Save();
                
                PlayerPrefs.SetFloat("HappyMarketTime", timeElapsed);
                PlayerPrefs.Save();

                resultText.text = "เยี่ยมเลย จำได้หมดแล้ว";
                
                if (ProgressData.instance != null)
                    ProgressData.instance.CompleteQuest("HappyMarket", 0);
                
                /*if (StreakController.instance != null)
                    StreakController.instance.AddStreak();*/
                
                Invoke(nameof(GoToStory2), 3f);
            }
        }
        else
        {
            textUI.color = Color.red;
            StartCoroutine(ResetTextColor(textUI));

            penaltyScore++;
            totalScore = 100 - penaltyScore;

            if (totalScore < 0)
                totalScore = 0;
            scoreText.text = "Score : " + totalScore;
        }
    }

    public HappyMarketStoryController storyController;

    void GoToStory2()
    {
        Debug.Log("GoToStory2 called!");
        
        panelSelect.SetActive(false);
        panelMemory.SetActive(false); // เพิ่ม
        
        // ปิด Panel_CheckListGame ทั้งหมดก่อน
        gameObject.SetActive(false); // เพิ่ม
        
        if (storyController != null)
        {
            storyController.StartNextStory();
        }
    }

    void GenerateSelectItems()
    {
        List<string> options = new();

        // ใส่ targetItems ก่อน
        foreach (string item in targetItems)
        {
            string cleanItem = item.Trim();

            if (!options.Contains(cleanItem))
            {
                options.Add(cleanItem);
            }
        }

        // สุ่มตัวหลอก
        List<string> temp = new();

        foreach (string item in allItems)
        {
            string cleanItem = item.Trim();

            if (!options.Contains(cleanItem))
            {
                temp.Add(cleanItem);
            }
        }

        // เพิ่มตัวหลอกดิ้ ไม่เพิ่มกุจะหลอกผีมึง
        for (int i = 0; i < 3 && temp.Count > 0; i++)
        {
            int rand = Random.Range(0, temp.Count);

            options.Add(temp[rand]);
            temp.RemoveAt(rand);
        }

        Shuffle(options);

        // ลบของเก่า
        foreach (string item in options)
        Debug.Log("option: " + item);

        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }

        // สร้างปุ่มใหม่
        foreach (string item in options)
        {
            GameObject obj = Instantiate(itemPrefab, gridParent);

            TextMeshProUGUI text = obj.GetComponentInChildren<TextMeshProUGUI>();
            text.text = item;
            text.color = Color.black;

            ItemButton btn = obj.GetComponent<ItemButton>();
            btn.itemName = item.Trim();
            btn.textUI = text;
            btn.game = this;
        }
    }

    void Shuffle(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            (list[i], list[rand]) = (list[rand], list[i]);
        }
        
    }

    IEnumerator ResetTextColor(TextMeshProUGUI textUI)
    {
        yield return new WaitForSeconds(1f);

        textUI.color = Color.black;
    }

    // =========================
    // STAGE 2: SHOPPING GAME
    // =========================
    void StartStage2()
    {
        panelSelect.SetActive(false);
        StartMemorizePhase();
    }

    public void StartMemorizePhase()
    {
        panelMemorize.SetActive(true);
        panelBudget.SetActive(false);
        panelShopping.SetActive(false);

        GenerateShoppingItems(3);
        currentCardIndex = 0;
        DisplayCurrentCard();

        nextButton.onClick.RemoveAllListeners();
        nextButton.onClick.AddListener(ShowNextCard);
    }

    void GenerateShoppingItems(int count)
    {
        shoppingTargetItems.Clear();

        List<MarketItem> temp = new(allMarketItems);
        count = Mathf.Min(count, temp.Count);

        for (int i = 0; i < count; i++)
        {
            int rand = Random.Range(0, temp.Count);
            shoppingTargetItems.Add(temp[rand]);
            temp.RemoveAt(rand);
        }
    }

    void DisplayCurrentCard()
    {
        if (currentCardIndex >= shoppingTargetItems.Count) return;

        currentCard.Setup(shoppingTargetItems[currentCardIndex]);
        cardCounterText.text = $"สินค้า {currentCardIndex + 1}/{shoppingTargetItems.Count}";
    }

    public void ShowNextCard()
    {
        currentCardIndex++;

        if (currentCardIndex < shoppingTargetItems.Count)
        {
            DisplayCurrentCard();
        }
        else
        {
            ShowBudgetPhase();
        }
    }

    void ShowBudgetPhase()
    {
        panelMemorize.SetActive(false);
        panelBudget.SetActive(true);

        playerBudget = Random.Range(50, 201);
        budgetText.text = $"งบของคุณวันนี้: {playerBudget} บาท";
    }

    public void StartShoppingPhase()
    {
        panelBudget.SetActive(false);
        panelShopping.SetActive(true);
    }
}


