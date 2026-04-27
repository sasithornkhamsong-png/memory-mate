using UnityEngine;
using TMPro;

public class ShopUIManager : MonoBehaviour
{
    [Header("Data")]
    public MarketItem[] marketItems;

    [Header("UI")]
    public Transform itemListParent;
    public ShoppingItemUI shoppingItemPrefab;
    public TextMeshProUGUI totalPriceText;

    private void Start()
    {
        GenerateShopItems();
        UpdateTotalPrice();
    }

    void GenerateShopItems()
    {
        foreach (MarketItem item in marketItems)
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
}
