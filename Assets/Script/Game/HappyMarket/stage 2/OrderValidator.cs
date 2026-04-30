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
    public MemoryCardManager memoryCardManager;

    private void Start()
    {
        GenerateRequiredItems();
    }

    // เอา budget ออก ไม่ต้องส่งมาแล้ว เพราะ ShopUIManager เช็คให้แล้ว
    public bool ValidateOrder(List<ShoppingItemUI> selectedItems)
    {
        if (selectedItems == null || selectedItems.Count == 0)
        {
            Debug.Log("ไม่ได้เลือกสินค้าเลย");
            return false;
        }

        foreach (RequiredItem required in requiredItems)
        {
            ShoppingItemUI foundItem = selectedItems.Find(
                item => item.GetItem().itemName == required.itemName
            );

            if (foundItem == null)
            {
                Debug.Log("ไม่มีสินค้า : " + required.itemName);
                return false;
            }

            if (foundItem.GetQuantity() < required.requiredQuantity)
            {
                Debug.Log("จำนวนไม่ถูกต้อง : " + required.itemName);
                return false;
            }
        }

        /*foreach (ShoppingItemUI selected in selectedItems)
        {
            bool isRequired = requiredItems.Exists(
                required => required.itemName == selected.GetItem().itemName
            );

            if (!isRequired)
            {
                Debug.Log("มีสินค้านอกเหนือจากรายการ : " + selected.GetItem().itemName);
                return false;
            }
        }*/
        foreach (ShoppingItemUI selected in selectedItems)
        {
            string selectedName = selected.GetItem().itemName;

            Debug.Log($"Selected: '{selectedName}'");
            foreach (RequiredItem req in requiredItems)
            {
                Debug.Log($"Required: '{req.itemName}' | Match: {req.itemName == selectedName}");
            }

            bool isRequired = requiredItems.Exists(
                required => required.itemName == selectedName
            );

            if (!isRequired)
            {
                Debug.Log("มีสินค้านอกเหนือจากรายการ : " + selectedName);
                return false;
            }
        }

        return true;
    }


    public void GenerateRequiredItems()
    {
        requiredItems.Clear();

        foreach (MemoryCardManager.MemoryItem item in memoryCardManager.items)
        {
            requiredItems.Add(new RequiredItem
            {
                itemName = item.itemName,
                requiredQuantity = 1
            });
        }
    }
}