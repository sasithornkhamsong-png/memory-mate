using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement; // ต้องใช้สำหรับการโหลดฉากใหม่

public class CardManager : MonoBehaviour
{
    public static CardManager instance;

    [Header("หน้าจอ UI")]
    public GameObject panelTutorial; 
    public GameObject panelWin; // ช่องสำหรับใส่หน้าสรุปผล

    [Header("การตั้งค่าตารางและการ์ด")]
    public GameObject cardPrefab;
    public Transform cardGrid;

    [Header("UI แสดงผล")]
    public TextMeshProUGUI scoreText; 
    public TextMeshProUGUI finalScoreText; // ข้อความคะแนนตอนจบ
    public TextMeshProUGUI finalTimeText;  // ข้อความเวลาตอนจบ

    [Header("คลังรูปภาพด้านหน้าการ์ด (ใส่มาอย่างน้อย 8 รูป)")]
    public List<Sprite> allCardFaces = new List<Sprite>();

    private List<Sprite> playCards = new List<Sprite>(); 

    private MemoryCard firstCard;
    private MemoryCard secondCard;

    private bool isChecking = false; 
    private int matchedPairs = 0; 

    public int totalScore = 100; 
    private int wrongMatchCount = 0; 

    private float timeElapsed = 0f; 
    private bool isTimerRunning = false; 

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        panelTutorial.SetActive(true);
        panelWin.SetActive(false); // ซ่อนหน้าสรุปผลไว้ก่อนตอนเริ่มเกม
        isTimerRunning = false; 
    }

    public void StartPlay()
    {
        panelTutorial.SetActive(false); 
        UpdateScoreText(); 
        SetupGame(); 
        isTimerRunning = true; 
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timeElapsed += Time.deltaTime;
        }
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + totalScore;
        }
    }

    void SetupGame()
    {
        if (allCardFaces.Count < 8) return;

        for (int i = 0; i < 8; i++)
        {
            playCards.Add(allCardFaces[i]);
            playCards.Add(allCardFaces[i]); 
        }

        for (int i = 0; i < playCards.Count; i++)
        {
            Sprite temp = playCards[i];
            int randomIndex = Random.Range(i, playCards.Count);
            playCards[i] = playCards[randomIndex];
            playCards[randomIndex] = temp;
        }

        for (int i = 0; i < 16; i++)
        {
            GameObject newCardObj = Instantiate(cardPrefab, cardGrid);
            MemoryCard cardScript = newCardObj.GetComponent<MemoryCard>();
            cardScript.SetupCard(playCards[i]); 
        }
    }

    public void CardClicked(MemoryCard clickedCard)
    {
        if (isChecking || clickedCard.isFlipped) return;

        clickedCard.Flip(true); 

        if (firstCard == null)
        {
            firstCard = clickedCard;
        }
        else
        {
            secondCard = clickedCard;
            StartCoroutine(CheckMatch()); 
        }
    }

    IEnumerator CheckMatch()
    {
        isChecking = true; 
        yield return new WaitForSeconds(1f); 

        if (firstCard.cardFaceSprite == secondCard.cardFaceSprite)
        {
            matchedPairs++;
            firstCard.GetComponent<Button>().interactable = false;
            secondCard.GetComponent<Button>().interactable = false;

            if (matchedPairs >= 8)
            {
                isTimerRunning = false; 
                ShowWinScreen(); // เรียกใช้ฟังก์ชันโชว์หน้าต่างตอนชนะ
            }
        }
        else
        {
            wrongMatchCount++; 

            if (wrongMatchCount >= 3)
            {
                totalScore--; 
                UpdateScoreText(); 
            }

            firstCard.Flip(false);
            secondCard.Flip(false);
        }

        firstCard = null;
        secondCard = null;
        isChecking = false; 
    }

// --- หน้าสรุปผล ---
    void ShowWinScreen()
    {
        panelWin.SetActive(true); // เปิดหน้าต่าง

        // --- บันทึกว่าเล่นจบไปอีก 1 รอบ ---
        int currentPlayCount = PlayerPrefs.GetInt("Mission_PlayCount", 0);
        PlayerPrefs.SetInt("Mission_PlayCount", currentPlayCount + 1);
        PlayerPrefs.Save();
        // ---------------------------------------------
        
        // *** 1. โหลดข้อมูลคะแนนและเวลาจากด่าน 1 (ถ้าไม่มีข้อมูลเลยให้เป็น 0 ไว้ก่อน) ***
        int level1Score = PlayerPrefs.GetInt("Level1Score", 0);
        float level1Time = PlayerPrefs.GetFloat("Level1Time", 0f);

        // *** 2. เอาข้อมูลของด่าน 1 มาบวกรวมกับข้อมูลด่าน 2 ปัจจุบัน ***
        int combinedScore = level1Score + totalScore;
        float combinedTime = level1Time + timeElapsed;
        
        // *** เรียกใช้ฟังก์ชันบันทึกและจัดอันดับ ***
        SaveAndSortHighScore(combinedScore, combinedTime);
        
        // *** 3. แสดงผลคะแนน "รวม" ***
        if (finalScoreText != null) 
            finalScoreText.text = combinedScore.ToString(); 
            
        // *** 4. แสดงผลเวลา "รวม" ***
        if (finalTimeText != null) 
        {
            // ใช้เวลาที่บวกรวมกันแล้ว (combinedTime) มาคำนวณแทน timeElapsed
            int totalSeconds = Mathf.FloorToInt(combinedTime); 
            
            int minutes = totalSeconds / 60; 
            int seconds = totalSeconds % 60; 

            if (minutes > 0)
                finalTimeText.text = minutes + " m " + seconds + " s";
            else
                finalTimeText.text = seconds + " s";
        }
    }

public void SaveAndSortHighScore(int currentScore, float currentTime)
    {
        // 1. โหลดข้อมูลเก่าที่เคยเซฟไว้
        string json = PlayerPrefs.GetString("Leaderboard", "");
        ScoreData data = new ScoreData();

        // ถ้าเคยมีข้อมูลเซฟไว้แล้ว ให้แปลงจาก JSON กลับมาเป็นข้อมูล
        if (!string.IsNullOrEmpty(json))
        {
            data = JsonUtility.FromJson<ScoreData>(json);
        }

        // *** เช็คว่าเคยมีสถิติอยู่บนกระดานหรือไม่ ***
        bool hasPreviousRecord = data.records.Count > 0;

        // 2. สร้างข้อมูลรอบใหม่นี้
        ScoreRecord newRecord = new ScoreRecord();
        newRecord.score = currentScore;
        newRecord.time = currentTime;
        newRecord.date = System.DateTime.Now.ToString("dd/MM/yyyy");

        // เพิ่มข้อมูลรอบใหม่เข้าไปใน List
        data.records.Add(newRecord);

        // 3. จัดอันดับ (Sorting Logic)
        data.records.Sort((a, b) => {
            if (a.score != b.score)
            {
                // ถ้าคะแนนไม่เท่ากัน เรียงจากคะแนน มาก -> น้อย
                return b.score.CompareTo(a.score); 
            }
            else
            {
                // ถ้าคะแนนเท่ากัน เรียงจากเวลา น้อย -> มาก
                return a.time.CompareTo(b.time); 
            }
        });

        // 4. เช็คว่าทำลายสถิติไหม
        // ต้องมีสถิติเดิมอยู่ก่อน (hasPreviousRecord) และ สถิติใหม่ต้องขึ้นอันดับ 1 ***
        if (hasPreviousRecord && data.records[0] == newRecord)
        {
            int topScoreCount = PlayerPrefs.GetInt("Mission_TopScoreCount", 0);
            PlayerPrefs.SetInt("Mission_TopScoreCount", topScoreCount + 1);
            PlayerPrefs.Save();
        }

        // 5. ตัดให้เหลือแค่ 5 อันดับแรก
        if (data.records.Count > 5)
        {
            data.records.RemoveAt(5); 
        }

        // 6. แปลงกลับเป็น JSON แล้วเซฟทับลง PlayerPrefs
        string newJson = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("Leaderboard", newJson);
        PlayerPrefs.Save();
    }

  // --- ปุ่มต่างๆ ในหน้า Win Panel ---

    public void PlayAgain()
    {
        // กลับไปด่าน 1
        SceneManager.LoadScene("Scene_Level1"); 
    }

    public void GoToHome()
    {
        // ไปหน้าโฮม
        SceneManager.LoadScene("Scene_Home"); 
    }

    public void GoToMission()
    {
        // ไปหน้าภารกิจ
        SceneManager.LoadScene("Scene_Mission"); 
    }

        public void GoToStatistic()
    {
        // ไปหน้าสถิติ
        SceneManager.LoadScene("Scene_Statistic"); 
    }
}

[System.Serializable] // เพื่อให้ Unity นำไปแปลงเป็น JSON และเซฟข้อมูลได้
public class ScoreRecord
{
    public int score;
    public float time;
    public string date;
}

[System.Serializable]
public class ScoreData
{
    // สร้างเป็น List เพื่อเก็บข้อมูล 5 อันดับ
    public List<ScoreRecord> records = new List<ScoreRecord>(); 
}