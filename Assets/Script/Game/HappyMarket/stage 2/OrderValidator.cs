using System.Collections.Generic;
using UnityEngine;

public class OrderValidator : MonoBehaviour
{
    [System.Serializable]
    public class RequiredItem
    {
        public string itemName;
        public int requiredQuantity;
    }

    public List<RequiredItem> requiredItems = new List<RequiredItem>();

    public bool ValidateOrder(List<ShoppingItemUI> selectedItems)
    {
        foreach (RequiredItem required in requiredItems)
        {
            ShoppingItemUI foundItem = selectedItems.Find(
                item => item.GetItem().itemName == required.itemName
            );

            if (foundItem == null)
            {
                Debug.Log("ไม่มีสินค้า: " + required.itemName);
                return false;
            }

            if (foundItem.GetQuantity() != required.requiredQuantity)
            {
                Debug.Log("จำนวนไม่ถูกต้อง: " + required.itemName);
                return false;
            }
        }

        return true;
    }
}