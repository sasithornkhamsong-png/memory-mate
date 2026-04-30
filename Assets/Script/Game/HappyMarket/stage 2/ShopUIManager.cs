using System.Collections.Generic;
using UnityEngine;
using TMPro;

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

    private void Start()
    {
        //playerBudget = memoryCardManager.GetCurrentBudget();
        GenerateShopItems();
        UpdateTotalPrice();
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
            Debug.Log("สั่งซื้อถูกต้อง!");
        else
            Debug.Log("สั่งซื้อไม่ถูกต้อง");
    }
}
