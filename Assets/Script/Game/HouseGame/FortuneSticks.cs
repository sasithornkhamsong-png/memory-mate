using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class FortuneSticksGame : MonoBehaviour
{
    [Header("Panels")]
    public GameObject panelStory;
    public GameObject panelRules;
    public GameObject panelShake;
    public GameObject panelShowNumber;
    public GameObject panelInput;
    public GameObject panelCongrats;

    [Header("Show Number UI")]
    public TextMeshProUGUI numberText;

    [Header("Input Phase UI")]
    public TextMeshProUGUI inputText;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI helpDisplayText;
    public GameObject helpButton;
    public GameObject helpImage;

    // ระบบจัดการตัวเลข
    private List<int> drawnNumbers = new List<int>();
    private int targetSum = 0;
    private int shakeCount = 0;
    private string playerInput = "";
    
    // ระบบเซนเซอร์และสถานะ
    private bool canShake = false;
    private float shakeThreshold = 2.5f; 
    private bool isHelpShowing = false;
    private int helpCount = 0;

    // ระบบเวลาและคะแนน
    private float totalTimeUsed = 0f;
    private bool isGameFinished = false;
    private int playerScore = 100;

    [Header("Congrats Phase UI")]
    public TextMeshProUGUI totalScoreText;
    public TextMeshProUGUI totalTimeText; 


    void Start()
    {
        ShowPanel(panelStory);
        playerScore = 100;
        drawnNumbers.Clear();
        shakeCount = 0;
        helpCount = 0;

        totalTimeUsed = 0f;
        isGameFinished = false;

        if (Accelerometer.current != null)
        {
            InputSystem.EnableDevice(Accelerometer.current);
        }
    }

    /*void Update()
    {
        if (canShake)
        {
            float shakeMagnitude = Input.acceleration.sqrMagnitude;
            if (shakeMagnitude > shakeThreshold || Input.GetKeyDown(KeyCode.Space))
            {
                GenerateNumber();
            }
        }
    }*/

    void Update()
{
    if (canShake)
    {
        bool isSpacePressed = false;
        bool isShaking = false;

        // 1. เช็คการกด Spacebar (สำหรับทดสอบในคอมพิวเตอร์)
        if (Keyboard.current != null && Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            isSpacePressed = true;
        }

        // 2. เช็คการเขย่า (สำหรับโทรศัพท์มือถือ)
        if (Accelerometer.current != null)
        {
            float shakeMagnitude = Accelerometer.current.acceleration.ReadValue().sqrMagnitude;
            if (shakeMagnitude > shakeThreshold)
            {
                isShaking = true;
            }
        }

        // ถ้ามีการเขย่า หรือ กด Spacebar ให้ทำการสุ่มเลข
        if (isShaking || isSpacePressed)
        {
            GenerateNumber();
        }
    }
    
        if (!isGameFinished)
        {
            if (panelShowNumber.activeInHierarchy || panelInput.activeInHierarchy)
            {
                totalTimeUsed += Time.deltaTime;
            }
        }
}

    // ================= ฟังก์ชันปุ่มสลับ Panel =================

    public void OnClickNextToRules()
    {
        ShowPanel(panelRules);
    }

    public void OnClickNextToShake()
    {
        ShowPanel(panelShake);
        canShake = true; // เปิดให้เริ่มเขย่าได้
    }

    private void ShowPanel(GameObject panelToShow)
    {
        panelStory.SetActive(false);
        panelRules.SetActive(false);
        panelShake.SetActive(false);
        panelShowNumber.SetActive(false);
        panelInput.SetActive(false);
        panelCongrats.SetActive(false);

        panelToShow.SetActive(true);
    }

    // ================= ระบบเขย่าและสุ่มเลข =================

    private void GenerateNumber()
    {
        canShake = false; 

        int newNumber = Random.Range(1, 101); 
        drawnNumbers.Add(newNumber);
        
        numberText.text = newNumber.ToString();

        // --- 3. เปลี่ยนไปแสดง Panel โชว์ตัวเลขแทน ---
        ShowPanel(panelShowNumber); 

        /*if (drawnNumbers.Count == 1)
        {
            startTime = Time.time;
            isTiming = true;
        }*/
    }

    public void OnClickRemembered()
    {
        shakeCount++;

        if (shakeCount < 3)
        {
            // --- 4. กลับไปหน้ากระบอกเซียมซี ---
            ShowPanel(panelShake);
            canShake = true; 
        }
        else
        {
            StartInputPhase();
        }
    }

    // ================= ระบบตอบคำถาม =================

    private void StartInputPhase()
    {
        ShowPanel(panelInput);
        
        targetSum = 0;
        foreach (int num in drawnNumbers)
        {
            targetSum += num;
        }

        playerInput = "";
        inputText.text = "";
        resultText.text = "";
        helpDisplayText.text = "";
    }

    public void PressNumber(string num)
    {
        if (playerInput.Length < 4) 
        {
            playerInput += num;
            inputText.text = playerInput;
            resultText.text = "";
        }
    }

    public void PressClear()
    {
        playerInput = "";
        inputText.text = "";
    }

    public void PressOK()
    {
        if (playerInput == "") return;

        int answer = int.Parse(playerInput);

        if (answer == targetSum)
        {
            isGameFinished = true; // หยุดจับเวลาทันที
            PlayerPrefs.SetFloat("time_Game1Level3", totalTimeUsed);
            PlayerPrefs.SetInt("score_Game1Level3", playerScore);
            PlayerPrefs.Save();

            resultText.text = "<color=green>RIGHT</color>";
            StartCoroutine(ShowCongratsAfterDelay());
        }
        else
        {
            resultText.text = "<color=red>try again</color>";
            playerScore--;
            if (playerScore < 0) playerScore = 0;
            PressClear();
            StartCoroutine(HideResultTextRoutine()); 
        }
    }

    IEnumerator ShowCongratsAfterDelay()
    {
        yield return new WaitForSeconds(3f);

        // 1. ดึงค่าคะแนนและเวลาจากด่าน 1 และ 2 (ใช้ 0 เป็นค่าเริ่มต้นถ้าหาไม่เจอ)
        int scoreL1 = PlayerPrefs.GetInt("score_Game1Level1", 0);
        int scoreL2 = PlayerPrefs.GetInt("score_Game1Level2", 0);
        int scoreL3 = playerScore; // คะแนนด่าน 3 ปัจจุบัน

        float timeL1 = PlayerPrefs.GetFloat("time_Game1Level1", 0f);
        float timeL2 = PlayerPrefs.GetFloat("time_Game1Level2", 0f);
        float timeL3 = totalTimeUsed; // เวลาด่าน 3 ปัจจุบัน

        // 2. คำนวณผลรวม
        int totalScore = scoreL1 + scoreL2 + scoreL3;
        float totalTime = timeL1 + timeL2 + timeL3;

        // ส่งค่าไป Progress
        ProgressData.instance.SaveGameResult(
        "HouseGame",
        totalScore,
        totalTime
    );

        // 3. แปลงเวลารวม (วินาที) ให้เป็นรูปแบบ นาที และ วินาที
        int minutes = Mathf.FloorToInt(totalTime / 60);
        int seconds = Mathf.FloorToInt(totalTime % 60);

        // 4. แสดงผลบน UI
        if (totalScoreText != null)
        {
            totalScoreText.text = totalScore.ToString();
        }

        if (totalTimeText != null)
        {
            totalTimeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        } 

        scoreGame1Manager.SaveEntry(totalScore, totalTime);

        //สะสมจำนวนครั้งที่เล่นจบเกมที่ 1
        int currentPlay = PlayerPrefs.GetInt("Game1_PlayCount", 0);
        PlayerPrefs.SetInt("Game1_PlayCount", currentPlay + 1);

        //บันทึกสถิติลง 5 อันดับแรก
        scoreGame1Manager.SaveEntry(totalScore, totalTime);
        PlayerPrefs.Save();
        
        ShowPanel(panelCongrats);
    }

    // ================= ระบบช่วยเหลือ (Help) =================

    public void OnClickHelp()
    {
        if (isHelpShowing) return; 

        helpCount++;
        
        if (helpCount >= 2)
        {
            playerScore--;
            if (playerScore < 0) playerScore = 0;
        }

        StartCoroutine(ShowHelpRoutine());
    }

    IEnumerator ShowHelpRoutine()
    {
        isHelpShowing = true;
        helpButton.SetActive(false); 

        // --- 2. สั่งเปิดทั้ง Image(ตัวแม่) และ Text(ตัวลูก) ---
        if (helpImage != null) helpImage.SetActive(true); 
        helpDisplayText.gameObject.SetActive(true); 

        // แสดงเลขทั้ง 3 ตัว
        helpDisplayText.text = drawnNumbers[0] + " , " + drawnNumbers[1] + " , " + drawnNumbers[2];
        
        yield return new WaitForSeconds(3f); 

        helpDisplayText.text = ""; 

        // --- 3. สั่งปิดทั้ง Image และ Text กลับไปซ่อนเหมือนเดิม ---
        helpDisplayText.gameObject.SetActive(false); 
        if (helpImage != null) helpImage.SetActive(false); 

        helpButton.SetActive(true); 
        isHelpShowing = false;
    }

    IEnumerator HideResultTextRoutine()
    {
        // รอเวลา 1 วินาที
        yield return new WaitForSeconds(1f);
        
        // ล้างข้อความทิ้ง
        resultText.text = "";
    }

    public void OnClickPlayAgain()
    {
        SceneManager.LoadScene("HouseGame");
    }

    public void OnClickHome()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void OnClickMission()
    {
        SceneManager.LoadScene("Scene_Mission");
    }

    public void OnClickStat()
    {
        SceneManager.LoadScene("stat_game1");
    }

}