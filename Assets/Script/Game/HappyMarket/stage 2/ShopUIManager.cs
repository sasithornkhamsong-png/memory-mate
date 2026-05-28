using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Collections;

public class ShopUIManager : MonoBehaviour
{
    [Header("Data")]
    public MarketItem[] marketItems;
    public MemoryCardManager memoryCardManager;
    public OrderValidator orderValidator;
    public int playerBudget = 100;
    public MarketItem[] allItems;

    [Header("UI")]
    public Transform itemListParent;
    public ShoppingItemUI shoppingItemPrefab;
    public TextMeshProUGUI totalPriceText;
    public HappyMarketStoryController storyController;
    public GameObject shoppingPanel;

    [Header("Score")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI resultText;
    private float timeElapsed = 0f;
    private bool isTimerRunning = true;

    private int totalScore = 100;
    private int penaltyScore = 0;

    private void Start()
    {
        totalScore = 100;
        scoreText.text = "Score : " + totalScore;
        
        //playerBudget = memoryCardManager.GetCurrentBudget();
        GenerateShopItems();
        UpdateTotalPrice();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timeElapsed += Time.deltaTime;
        }
    }

    void GenerateShopItems()
    {
        List<MarketItem> shopItems = memoryCardManager.GetShoppingItems();

        foreach (MarketItem item in shopItems)
        {
            ShoppingItemUI newItem = Instantiate(shoppingItemPrefab, itemListParent);
            newItem.Setup(item, UpdateTotalPrice);
        }
    }

    public void UpdateTotalPrice()
    {
        if (totalPriceText == null)
        {
            Debug.LogError("Total Price Text is not assigned!");
            return;
        }

        int total = 0;

        ShoppingItemUI[] allItems = itemListParent.GetComponentsInChildren<ShoppingItemUI>();

        foreach (ShoppingItemUI itemUI in allItems)
        {
            if (itemUI != null)
            {
                total += itemUI.GetTotalPrice();
            }
        }

        totalPriceText.text = total + " บาท";
    }

    /*public void UpdateTotalPrice()
    {
        int total = 0;

        ShoppingItemUI[] items = itemListParent.GetComponentsInChildren<ShoppingItemUI>();

        foreach (ShoppingItemUI item in items)
        {
            total += item.GetTotalPrice();
        }

        totalPriceText.text = "ยอดรวม: " + total + " บาท";
    }*/

   public void ConfirmOrder()
    {
        List<ShoppingItemUI> selectedItems = new List<ShoppingItemUI>();

        ShoppingItemUI[] allItemUIs = itemListParent.GetComponentsInChildren<ShoppingItemUI>();

        foreach (ShoppingItemUI itemUI in allItemUIs)
        {
            if (itemUI.GetQuantity() > 0)
            {
                selectedItems.Add(itemUI);
            }
        }

        int totalCost = 0;
        foreach (ShoppingItemUI itemUI in selectedItems)
        {
            totalCost += itemUI.GetTotalPrice();
        }

        if (totalCost > memoryCardManager.GetCurrentBudget())
        {
            Debug.Log("งบไม่พอ!");
            return;
        }

        bool isCorrect = orderValidator.ValidateOrder(selectedItems);

        if (isCorrect)
        {   resultText.text = "ซื้อของครบแล้ว!";
            resultText.color = Color.green;

            isTimerRunning = false;

            PlayerPrefs.SetFloat("ShoppingGameTime", timeElapsed);
            PlayerPrefs.Save();

            PlayerPrefs.SetInt("ShoppingGameScore", totalScore);
            PlayerPrefs.Save();

            Invoke(nameof(GoToNextStory), 3f);

            Debug.Log("สั่งซื้อถูกต้อง!");

            ProgressData.instance.CompleteQuest("HappyMarket", 1);
            ProgressData.instance.UpdateBestScore("HappyMarket", totalCost);
            //StreakController.instance.AddStreak();
        }
        else
        {
            resultText.text = "ซื้อผิดรายการ!";
            StartCoroutine(HideResultText());
            resultText.color = Color.red;

            penaltyScore++;

            totalScore = 100 - penaltyScore;

            if (totalScore < 0)
                totalScore = 0;

            scoreText.text = "Score : " + totalScore;

            Debug.Log("สั่งซื้อไม่ถูกต้อง");
        }
    }
        void GoToNextStory()
        {
            shoppingPanel.SetActive(false);

            if (storyController != null)
            {
                storyController.StartNextStory();
            }
        }

    IEnumerator HideResultText()
    {
        yield return new WaitForSeconds(1f);

        resultText.text = "";
        resultText.color = Color.white;
    }
        
}
