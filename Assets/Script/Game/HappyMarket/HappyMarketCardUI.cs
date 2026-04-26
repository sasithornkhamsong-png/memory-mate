using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

[Serializable]
public class MarketItem
{
    public string itemName;
    public Sprite itemSprite;
    public int price;
    public int weight;
}

public class HappyMarketCardUI : MonoBehaviour
{
    [Header("UI")]
    public Image itemImage;
    public TMP_Text itemNameText;
    public TMP_Text itemInfoText;

    public void Setup(MarketItem item)
    {
        itemImage.sprite = item.itemSprite;
        itemNameText.text = item.itemName;
        itemInfoText.text = $"ปริมาณ {item.weight} กรัม\nราคา {item.price} บาท";
    }
}