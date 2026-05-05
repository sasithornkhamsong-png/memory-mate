using UnityEngine;
using UnityEngine.UI;

public class ItemClick : MonoBehaviour
{
    private Image myImage;
    private Button myButton;

    void Start()
    {
        // ค้นหาส่วนประกอบ Image และ Button ที่อยู่ในการ์ดใบนี้
        myImage = GetComponent<Image>();
        myButton = GetComponent<Button>();

        // สั่งให้ปุ่มทำงานอัตโนมัติเมื่อถูกคลิก (ไม่ต้องไปลากเส้นใน Inspector ให้เมื่อย!)
        myButton.onClick.AddListener(OnCardClicked);
    }

    void OnCardClicked()
    {
        // พอถูกคลิก ให้ส่งรูปภาพของตัวเองไปให้ Level1Manager ช่วยตรวจ
        Level1Manager.instance.CheckClickedItem(myImage.sprite, this.gameObject);
    }
}