using UnityEngine;
using UnityEngine.UI;

public class MemoryCard : MonoBehaviour
{
    [Header("ส่วนประกอบของการ์ด")]
    public Image frontImage; // ช่องสำหรับใส่รูปด้านหน้า
    public GameObject backImageObj; // ช่องสำหรับใส่ลายหลังการ์ด

    [HideInInspector]
    public Sprite cardFaceSprite; // เก็บข้อมูลรูปด้านหน้าของตัวเองเอาไว้
    
    [HideInInspector]
    public bool isFlipped = false; // เช็คว่าตอนนี้การ์ดหงายอยู่หรือไม่

    void Start()
    {
        // สั่งให้ปุ่มเรียกฟังก์ชัน OnCardClicked เมื่อถูกกด
        GetComponent<Button>().onClick.AddListener(OnCardClicked);
    }

    // CardManager จะส่งรูปหน้าการ์ดมาให้ตอนเริ่มเกม
    public void SetupCard(Sprite faceSprite)
    {
        cardFaceSprite = faceSprite;
        frontImage.sprite = cardFaceSprite; // เอารูปมาใส่ที่ด้านหน้า
        Flip(false); // เริ่มต้นมาให้คว่ำการ์ดไว้ (false = คว่ำ)
    }

    // ฟังก์ชันสั่ง หงาย/คว่ำ การ์ด
    public void Flip(bool state)
    {
        isFlipped = state;
        backImageObj.SetActive(!state); // ถ้า state คือ true (หงาย) จะปิดการแสดงผลของหลังการ์ด ทำให้เห็นรูปด้านหน้า
    }

    void OnCardClicked()
    {
        // เมื่อถูกคลิก ให้สะกิดบอก CardManager ว่า "ถูกคลิกแล้ว!"
        CardManager.instance.CardClicked(this);
    }
}