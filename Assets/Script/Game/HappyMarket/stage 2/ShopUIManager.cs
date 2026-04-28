using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShopUIManager : MonoBehaviour
{
    [Header("Data")]
    public MarketItem[] marketItems;
    public OrderValidator orderValidator;
    public int playerBudget = 100;
    public MarketItem[] allItems;

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
        List<MarketItem> shopItems = new List<MarketItem>();

        // เพิ่มสินค้าที่ต้องซื้อ
        shopItems.AddRange(marketItems);

        // หาสินค้าที่ไม่ได้อยู่ในรายการ
        List<MarketItem> extraItems = new List<MarketItem>();

        //foreach (MarketItem item in FindObjectsByType<ShopItemDatabase>(FindObjectsSortMode.None)[0].allItems)
        foreach (MarketItem item in allItems)
        {
            if (!shopItems.Contains(item))
            {
                extraItems.Add(item);
            }
        }

        // เพิ่มสินค้าหลอก 3 ชิ้น
        for (int i = 0; i < 3 && extraItems.Count > 0; i++)
        {
            int randomIndex = Random.Range(0, extraItems.Count);
            shopItems.Add(extraItems[randomIndex]);
            extraItems.RemoveAt(randomIndex);
        }

        // สลับลำดับ
        for (int i = 0; i < shopItems.Count; i++)
        {
            MarketItem temp = shopItems[i];
            int randomIndex = Random.Range(i, shopItems.Count);
            shopItems[i] = shopItems[randomIndex];
            shopItems[randomIndex] = temp;
        }

        // สร้าง UI
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

        foreach (ShoppingItemUI itemUI in selectedItems) // ใช้ชื่อลิสต์จริงของโบ
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

        if (totalCost > playerBudget)
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
