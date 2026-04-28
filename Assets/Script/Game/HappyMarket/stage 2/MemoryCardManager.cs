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

    public Image itemImage;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI priceText;

    public GameObject memorizePanel;
    public GameObject budgetPanel;
    public GameObject shoppingPanel;

    public TMPro.TextMeshProUGUI budgetText;
    public TMPro.TextMeshProUGUI budgetRemainText;

    private int currentBudget;

    private int currentIndex = 0;

    void Start()
    {
        ShowItem();
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

    void ShowItem()
    {
        MemoryItem currentItem = items[currentIndex];

        itemImage.sprite = currentItem.image;
        itemNameText.text = currentItem.itemName;
        priceText.text = "ราคา " + currentItem.price + " บาท";
    }

    void GenerateBudget()
    {
        currentBudget = Random.Range(80, 301); // 80 ถึง 300

        budgetText.text = currentBudget + " บาท";
        budgetRemainText.text = "เงินในกระเป๋า: " + currentBudget + " บาท";
    }
    
    public void GoToShopping()
    {
        budgetPanel.SetActive(false);
        shoppingPanel.SetActive(true);
    }
}