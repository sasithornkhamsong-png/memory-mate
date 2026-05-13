using UnityEngine;
using TMPro;

public class AdditionCalculator : MonoBehaviour
{
    [Header("UI Reference")]
    public TextMeshProUGUI displayText; // หน้าจอแสดงผลเครื่องคิดเลข

    private int currentTotal = 0;       // เก็บผลรวมที่บวกสะสมไว้
    private string currentInput = "";   // เก็บตัวเลขที่กำลังพิมพ์

    void OnEnable()
    {
        // รีเซ็ตค่าทุกครั้งที่เปิด Panel นี้ขึ้นมา
        PressClear();
    }

    // ฟังก์ชันสำหรับผูกกับปุ่ม 0-9
    public void PressNumber(string number)
    {
        currentInput += number;
        UpdateDisplay();
    }

    // ฟังก์ชันสำหรับผูกกับปุ่ม +
    public void PressPlus()
    {
        if (!string.IsNullOrEmpty(currentInput))
        {
            currentTotal += int.Parse(currentInput); // นำเลขที่พิมพ์ไปบวกเข้ากับยอดรวม
            currentInput = ""; // ล้างช่องพิมพ์เพื่อรอรับเลขตัวต่อไป
            displayText.text = currentTotal.ToString() + " + "; // แสดงผล เช่น "15 + "
        }
    }

    // ฟังก์ชันสำหรับผูกกับปุ่ม =
    public void PressEquals()
    {
        if (!string.IsNullOrEmpty(currentInput))
        {
            currentTotal += int.Parse(currentInput);
            currentInput = "";
        }
        
        // แสดงผลลัพธ์สุดท้าย
        displayText.text = currentTotal.ToString();
    }

    // ฟังก์ชันสำหรับผูกกับปุ่ม C (Clear)
    public void PressClear()
    {
        currentTotal = 0;
        currentInput = "";
        displayText.text = "0";
    }

    // อัปเดตหน้าจอเวลาผู้เล่นกำลังพิมพ์เลข
    private void UpdateDisplay()
    {
        if (currentTotal > 0)
        {
            // ถ้ามีการกดบวกไปแล้ว ให้แสดงผลรวมเดิม แล้วตามด้วยเลขที่กำลังพิมพ์
            displayText.text = currentTotal.ToString() + " + " + currentInput;
        }
        else
        {
            // ถ้ายังไม่เคยกดบวก ให้แสดงแค่เลขที่กำลังพิมพ์
            displayText.text = currentInput;
        }
    }
    
    // ฟังก์ชันเสริม: เผื่อต้องการดึงค่าผลลัพธ์สุดท้ายไปใช้ตรวจคำตอบ
    public int GetFinalResult()
    {
        return currentTotal;
    }
}