using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class MemoryCardManager : MonoBehaviour
{
    [System.Serializable]
    public class MemoryItem
    {
        public Sprite image;
        public string itemName;
        public int price;
    }

    public List<MemoryItem> items = new List<MemoryItem>();
    public List<MarketItem> allItems = new List<MarketItem>();

    [Header("Memorize UI")]
    public Image itemImage;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI priceText;

    [Header("Panels")]
    public GameObject memorizePanel;
    public GameObject budgetPanel;
    public GameObject shoppingPanel;

    [Header("Budget UI")]
    public TextMeshProUGUI budgetText;
    public TextMeshProUGUI budgetRemainText;

    private int currentBudget;
    private int currentIndex = 0;

    private void Start()
    {
        if (items.Count > 0)
        {
            GenerateMemoryList();
            ShowItem();
        }
    }

    public void NextItem()
    {
        currentIndex++;

        if (currentIndex < items.Count)
        {
            ShowItem();
        }
        else
        {
            memorizePanel.SetActive(false);
            budgetPanel.SetActive(true);
            GenerateBudget();
        }
    }

    void GenerateMemoryList()
    {
        List<MarketItem> tempList = new List<MarketItem>(allItems);
        items.Clear();

        int itemCount = Random.Range(3, 5); // 3–4 ชิ้น

        for (int i = 0; i < itemCount && tempList.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, tempList.Count);
            MarketItem selected = tempList[randomIndex];

            items.Add(new MemoryItem
            {
                image = selected.itemSprite,
                itemName = selected.itemName,
                price = selected.price
            });

            tempList.RemoveAt(randomIndex);
        }
    }

    private void ShowItem()
    {
        MemoryItem currentItem = items[currentIndex];

        itemImage.sprite = currentItem.image;
        itemNameText.text = currentItem.itemName;
        priceText.text = "ราคา " + currentItem.price + " บาท";
    }

    private void GenerateBudget()
    {
        currentBudget = Random.Range(80, 301);

        budgetText.text = currentBudget + " บาท";
        budgetRemainText.text = "เงินในกระเป๋า: " + currentBudget + " บาท";
    }

    public void GoToShopping()
    {
        budgetPanel.SetActive(false);
        shoppingPanel.SetActive(true);
    }

    public List<MarketItem> GetShoppingItems()
    {
        List<MarketItem> result = new List<MarketItem>();

        // เพิ่มสินค้าที่ต้องซื้อ
        foreach (MemoryItem memoryItem in items)
        {
            MarketItem matchedItem = allItems.Find(item => item.itemName == memoryItem.itemName);

            if (matchedItem != null)
            {
                result.Add(matchedItem);
            }
        }

        // หาสินค้าหลอก
        List<MarketItem> extraItems = allItems.FindAll(item => !result.Contains(item));

        // เพิ่มสินค้าหลอก 3 ชิ้น
        for (int i = 0; i < 3 && extraItems.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, extraItems.Count);
            result.Add(extraItems[randomIndex]);
            extraItems.RemoveAt(randomIndex);
        }

        // สุ่มลำดับสินค้า
        for (int i = 0; i < result.Count; i++)
        {
            int randomIndex = Random.Range(i, result.Count);
            MarketItem temp = result[i];
            result[i] = result[randomIndex];
            result[randomIndex] = temp;
        }

        return result;
    }

    public int GetCurrentBudget()
    {
        return currentBudget;
    }
}
