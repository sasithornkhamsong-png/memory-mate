using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Collections;

[System.Serializable]
public class Guest
{
    public string name;
    public string like;
    public string allergy;
    public Sprite image;
}

[System.Serializable]
public class Menu
{
    public string menuName;
    public string ingredient;
    public Sprite image;
}

public class PartyGame : MonoBehaviour
{
    [Header("UI - Info Panel")]
    public GameObject panelInfo;
    public TextMeshProUGUI infoText;
    public Image guestImage;

    [Header("UI - Menu Panel")]
    public GameObject panelMenu;
    public TextMeshProUGUI menuText;
    public Image foodImage;

    [Header("UI - Select Panel")]
    public GameObject panelSelect;

    [Header("Data")]
    public List<Guest> guests = new List<Guest>();
    public List<Menu> menus = new List<Menu>();

    private int currentIndex = 0;
    private List<Guest> selectedGuests = new List<Guest>();
    private Menu currentMenu;

    [Header("Score")]
    public TextMeshProUGUI scoreText;
    private int totalScore = 100;
    private int penaltyScore = 0; // เก็บแต้มที่โดนหักสะสมตลอดเกม

    [Header("Final Score")]
    private float timeElapsed = 0f;
    private bool isTimerRunning = false;

    [Header("Final Result")]
    public TextMeshProUGUI finalScoreText;
    public TextMeshProUGUI finalTimeText;

    public GameObject popupCorrect;
    public GameObject popupWrong;
    public GameObject panelScore;

    private bool gameFinished = false; // ป้องกัน ShowCorrect ถูกเรียกซ้ำ
    private bool isChoosing = false;

    // =========================
    // Start
    // =========================
    void Start()
    {
        panelInfo.SetActive(true);
        panelMenu.SetActive(false);
        panelSelect.SetActive(false);

        popupCorrect.SetActive(false);
        popupWrong.SetActive(false);

        scoreText.text = "Score: " + totalScore;
        isTimerRunning = true;

        GenerateGuests();
        ShowGuest();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timeElapsed += Time.deltaTime;
        }
    }

    // =========================
    // แสดงแขก
    // =========================
    void GenerateGuests()
    {
        selectedGuests.Clear();

        List<Guest> temp = new List<Guest>(guests);

        for (int i = 0; i < 4; i++)
        {
            int rand = Random.Range(0, temp.Count);
            selectedGuests.Add(temp[rand]);
            temp.RemoveAt(rand);
        }
    }

    void ShowGuest()
    {
        Guest g = selectedGuests[currentIndex];

        infoText.text =
            "คุณ " + g.name +
            "\n\nชอบ: " + g.like +
            "\nแพ้/ไม่ชอบ : " + g.allergy;

        guestImage.sprite = g.image;
    }

    // =========================
    // กด Next (ตอนจำแขก)
    // =========================
    public void OnNext()
    {
        currentIndex++;

        if (currentIndex >= selectedGuests.Count)
        {
            GoToMenu();
            return;
        }

        ShowGuest();
    }

    // =========================
    // ไปหน้าเมนู + สุ่มเมนู
    // =========================
    void GoToMenu()
    {
        panelInfo.SetActive(false);
        panelMenu.SetActive(true);
        panelSelect.SetActive(false);

        currentMenu = menus[Random.Range(0, menus.Count)];

        menuText.text =
            currentMenu.menuName +
            "\nมีส่วนผสม: " + currentMenu.ingredient;

        foodImage.sprite = currentMenu.image;
    }

    // =========================
    // เช็คว่าแขกคนนี้กินได้ไหม
    // =========================
    public bool CanServe(Guest g)
    {
        return !currentMenu.ingredient.Contains(g.allergy);
    }

    // =========================
    // กดเลือกแขก
    // =========================
    public void OnGuestSelected(int index)
    {
        // กันกดซ้ำ
        if (isChoosing || gameFinished)
            return;

        isChoosing = true;

        Guest g = selectedGuests[index];

        if (CanServe(g))
        {
            Debug.Log("CORRECT!");
            StartCoroutine(ShowCorrect());
        }
        else
        {
            Debug.Log("WRONG!");

            penaltyScore++;

            totalScore = 100 - penaltyScore;

            scoreText.text = "Score: " + totalScore;

            StartCoroutine(ShowWrong());
        }
}

    // =========================
    // Win
    // =========================
    void WinGame()
    {
        isTimerRunning = false;

        // คำนวณคะแนนด่านนี้จาก penalty ที่สะสมมา
        totalScore = 100 - penaltyScore;

        // โหลดคะแนนจากด่าน 1 และ 2
        int level1Score = PlayerPrefs.GetInt("Level1Score", 0);
        int level2Score = PlayerPrefs.GetInt("Level2Score", 0);
        float level1Time = PlayerPrefs.GetFloat("Level1Time", 0f);
        float level2Time = PlayerPrefs.GetFloat("Level2Time", 0f);

        // รวมทั้ง 3 ด่าน
        int finalScore = level1Score + level2Score + totalScore;
        float finalTime = level1Time + level2Time + timeElapsed;

        // Save
        PlayerPrefs.SetInt("FinalScore", finalScore);
        PlayerPrefs.SetFloat("FinalTime", finalTime);
        PlayerPrefs.Save();

        // แสดงผล
        int minutes = Mathf.FloorToInt(finalTime / 60);
        int seconds = Mathf.FloorToInt(finalTime % 60);

        finalScoreText.text = finalScore.ToString();
        finalTimeText.text = minutes + " m " + seconds + " s";

        //สะสมจำนวนครั้งที่เล่นจบเกมที่ 2
        int currentPlay = PlayerPrefs.GetInt("Game2_PlayCount", 0);
        PlayerPrefs.SetInt("Game2_PlayCount", currentPlay + 1);

        //บันทึกสถิติลง 5 อันดับแรก
        scoreGame2Manager.SaveEntry(finalScore, finalTime);
        PlayerPrefs.Save();

        if (panelScore != null)
            panelScore.SetActive(true);
    }

    // =========================
    // Setup Selection UI
    // =========================
    public Image[] guestSlots;
    public TextMeshProUGUI[] guestNames;

    void SetupSelectionUI()
    {
        for (int i = 0; i < guestSlots.Length; i++)
        {
            guestSlots[i].sprite = selectedGuests[i].image;
            guestNames[i].text = selectedGuests[i].name;
        }
    }

    public void GoToSelect()
    {
        panelInfo.SetActive(false);
        panelMenu.SetActive(false);
        panelSelect.SetActive(true);

        SetupSelectionUI();
    }

    // =========================
    // Popup Correct / Wrong
    // =========================
    IEnumerator ShowCorrect()
    {
        if (gameFinished) yield break;

        gameFinished = true;

        popupCorrect.SetActive(true);

        yield return new WaitForSeconds(5f);

        popupCorrect.SetActive(false);

        isChoosing = false;

        WinGame();
    }
    
    IEnumerator ShowWrong()
    {
        popupWrong.SetActive(true);

        yield return new WaitForSeconds(5f);

        popupWrong.SetActive(false);

        isChoosing = false;

        RestartGame();
    }

    // =========================
    // Restart (เฉพาะด่านนี้)
    // =========================
    void RestartGame()
    {
        currentIndex = 0;
        timeElapsed = 0f;    // reset เวลาของด่านนี้
        gameFinished = false; // reset flag

        // penaltyScore ไม่ reset! สะสมต่อไปเรื่อยๆ
        scoreText.text = "Score: " + (100 - penaltyScore);

        panelInfo.SetActive(true);
        panelMenu.SetActive(false);
        panelSelect.SetActive(false);

        GenerateGuests();
        ShowGuest();

        currentMenu = null;
    }
}