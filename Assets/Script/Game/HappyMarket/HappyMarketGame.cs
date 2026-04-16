using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class HappyMarketGame : MonoBehaviour
{
    [Header("UI")]
    public GameObject panelMemory;     // หน้าให้จำ
    public GameObject panelSelect;     // หน้าเลือก

    public TextMeshProUGUI memoryText; // list ตอนจำ

    [Header("Item UI")]
    public Transform gridParent;
    public GameObject itemPrefab;

    [Header("Data")]
    public List<string> allItems = new List<string>();     // item ทั้งหมด
    public List<string> targetItems = new List<string>();  // item ที่ต้องจำ

    private List<string> selectedItems = new List<string>();
    private int correctCount = 0;

    void Start()
    {
        SetupGame();
    }

    // =========================
    // สร้างเกม
    // =========================
    void SetupGame()
    {
        panelMemory.SetActive(true);
        panelSelect.SetActive(false);

        GenerateTargetItems(3); // เริ่ม 4 อัน

        ShowMemoryList();
    }

    // =========================
    // สุ่มของที่ต้องจำ
    // =========================
    void GenerateTargetItems(int count)
    {
        targetItems.Clear();

        List<string> temp = new List<string>(allItems);

        for (int i = 0; i < count; i++)
        {
            int rand = Random.Range(0, temp.Count);
            targetItems.Add(temp[rand]);
            temp.RemoveAt(rand);
        }
    }

    // =========================
    // แสดงรายการให้จำ
    // =========================
    void ShowMemoryList()
    {
        memoryText.text = "";

        foreach (string item in targetItems)
        {
            memoryText.text += "• " + item + "\n";
        }
    }

    // =========================
    // กด "จำเสร็จ"
    // =========================
    public void OnStartSelect()
    {
        panelMemory.SetActive(false);
        panelSelect.SetActive(true);

        ResetSelection();
        GenerateSelectItems();
    }

    // =========================
    // รีเซ็ตสถานะ
    // =========================
    void ResetSelection()
    {
        selectedItems.Clear();
        correctCount = 0;
    }

    // =========================
    // กด item
    // =========================
    public void OnItemClicked(string item, TextMeshProUGUI textUI)
    {
        if (selectedItems.Contains(item)) return;

        selectedItems.Add(item);

        if (targetItems.Contains(item))
        {
            textUI.color = Color.green;
            correctCount++;

            if (correctCount >= targetItems.Count)
            {
                Invoke("WinGame", 0.5f);
            }
        }
        else
        {
            textUI.color = Color.red;
        }
    }

    void GenerateSelectItems()
    {
        List<string> options = new List<string>(targetItems);

        // เอาของที่ไม่ใช่มาเป็นตัวหลอก
        List<string> temp = new List<string>(allItems);

        foreach (string item in targetItems)
            temp.Remove(item);

        for (int i = 0; i < 3; i++)
        {
            int rand = Random.Range(0, temp.Count);
            options.Add(temp[rand]);
            temp.RemoveAt(rand);
        }

        // สุ่มตำแหน่ง
        Shuffle(options);

        // ลบของเก่า
        foreach (Transform child in gridParent)
        {
            Destroy(child.gameObject);
        }

        // สร้างใหม่
        foreach (string item in options)
        {
            GameObject obj = Instantiate(itemPrefab, gridParent);

            var text = obj.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            text.color = Color.white; // 🔥 reset สี
            text.text = item;

            var btn = obj.GetComponent<ItemButton>();
            btn.itemName = item;
            btn.textUI = text;
            btn.game = this;
        }
    }

    void Shuffle(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            int rand = Random.Range(i, list.Count);
            string temp = list[i];
            list[i] = list[rand];
            list[rand] = temp;
        }
    }

    // =========================
    // ชนะ
    // =========================
    void WinGame()
    {
        Debug.Log("WIN!");
        // TODO: ไปด่านต่อไป
    }
}