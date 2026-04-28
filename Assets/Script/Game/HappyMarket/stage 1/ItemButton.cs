using UnityEngine;
using TMPro;

public class ItemButton : MonoBehaviour
{
    public string itemName;              // ชื่อ item
    public TextMeshProUGUI textUI;      // ตัวข้อความ
    public HappyMarketGame game;        // ตัวเกมหลัก

    public void OnClick()
    {
        game.OnItemClicked(itemName, textUI);
    }
}
