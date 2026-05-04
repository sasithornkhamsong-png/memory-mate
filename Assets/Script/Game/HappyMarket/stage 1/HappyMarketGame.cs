using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class HappyMarketGame : MonoBehaviour
{
    [Header("Stage 1 - Memory Game")]
    public GameObject panelMemory;
    public GameObject panelSelect;
    public TextMeshProUGUI memoryText;
    public Transform gridParent;
    public GameObject itemPrefab;

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

    private readonly List<MarketItem> shoppingTargetItems = new();
    private int currentCardIndex = 0;
    private int playerBudget = 0;

    private void Start()
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
        panelMemory.SetActive(false);
        panelSelect.SetActive(true);

        ResetSelection();
        GenerateSelectItems();
    }

    void ResetSelection()
    {
        selectedItems.Clear();
        correctCount = 0;
    }

    public void OnItemClicked(string item, TextMeshProUGUI textUI)
    {
        if (selectedItems.Contains(item)) return;

        selectedItems.Add(item);

        if (targetItems.Contains(item))
        {
            textUI.color = Color.green;
            correctCount++;

            if (correctCount >= targetItems.Count)
            {
                ProgressData.instance.CompleteQuest("HappyMarket", 0);
                Invoke(nameof(StartStage2), 0.5f);
            }
        }
        else
        {
            textUI.color = Color.red;
        }
    }

    void GenerateSelectItems()
    {
        List<string> options = new(targetItems);
        List<string> temp = new(allItems);

        foreach (string item in targetItems)
            temp.Remove(item);

        for (int i = 0; i < 3 && temp.Count > 0; i++)
        {
            int rand = Random.Range(0, temp.Count);
            options.Add(temp[rand]);
            temp.RemoveAt(rand);
        }

        Shuffle(options);

        foreach (Transform child in gridParent)
            Destroy(child.gameObject);

        foreach (string item in options)
        {
            GameObject obj = Instantiate(itemPrefab, gridParent);

            TextMeshProUGUI text = obj.GetComponentInChildren<TextMeshProUGUI>();
            text.color = Color.white;
            text.text = item;

            ItemButton btn = obj.GetComponent<ItemButton>();
            btn.itemName = item;
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


