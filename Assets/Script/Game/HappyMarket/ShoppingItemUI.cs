using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShoppingItemUI : MonoBehaviour
{
    [Header("UI References")]
    public Image itemImage;
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemPriceText;
    public TextMeshProUGUI quantityText;

    [Header("Buttons")]
    public Button minusButton;
    public Button plusButton;

    private MarketItem currentItem;
    private int quantity = 0;
    private System.Action onQuantityChanged;

    private void Start()
    {
        minusButton.onClick.AddListener(DecreaseQuantity);
        plusButton.onClick.AddListener(IncreaseQuantity);
        UpdateQuantityUI();
    }

    public void Setup(MarketItem item, System.Action quantityChangedCallback = null)
    {
        currentItem = item;
        onQuantityChanged = quantityChangedCallback;

        itemImage.sprite = item.itemSprite;
        itemNameText.text = item.itemName;
        itemPriceText.text = $"{item.price} บาท";

        quantity = 0;
        UpdateQuantityUI();
    }

    public void IncreaseQuantity()
    {
        quantity++;
        UpdateQuantityUI();
        onQuantityChanged?.Invoke();
    }

    public void DecreaseQuantity()
    {
        if (quantity <= 0) return;

        quantity--;
        UpdateQuantityUI();
        onQuantityChanged?.Invoke();
    }

    private void UpdateQuantityUI()
    {
        quantityText.text = quantity.ToString();
        minusButton.interactable = quantity > 0;
    }

    public int GetTotalPrice()
    {
        return currentItem.price * quantity;
    }

    public int GetQuantity()
    {
        return quantity;
    }

    public MarketItem GetItem()
    {
        return currentItem;
    }
}
