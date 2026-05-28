//Level1Manager ในโปรเจครวม 
using UnityEngine;
using UnityEngine.UI;
using System.Collections; // เพิ่มการใช้งาน Coroutine
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using TMPro; 

public class Level1Manager : MonoBehaviour
{
    public static Level1Manager instance; 

    [Header("หน้าจอต่างๆ")]
    public GameObject panelTutorial1;
    public GameObject panelRemember;
    public GameObject panelTutorial2;
    public GameObject panelPlay;
    public GameObject panelNextGame; 
    public GameObject panelCompliment;

    [Header("UI แสดงผล")]
    public TextMeshProUGUI scoreText; 
    public TextMeshProUGUI tutorialText;
    public TextMeshProUGUI tutorialText2;

    [Header("ระบบสร้างสิ่งของ")]
    public GameObject itemPrefab;
    public Transform gridParentRemember;
    public Transform gridParentPlay;

    [Header("คลังรูปภาพทั้งหมด")]
    public List<Sprite> allItemImages = new List<Sprite>();

    public static List<Sprite> rememberedItems = new List<Sprite>();
    public List<Sprite> playItems = new List<Sprite>(); 

    private bool isTimerRunning = false;
    private float timeElapsed = 0f; 
    
    public int totalScore = 0;  
    private int foundCount = 0; 

    void Awake()
    {
        instance = this; 
    }

    void Start()
    {
        tutorialText.text = "เข้าบ้านมาใหม่ คุณจะต้องจัดการสัมภาระให้เรียบร้อย จำเอาไว้ให้ดี ของทั้งหมดคือของของคุณ";
        ShowTutorial1();
    }

    void Update()
    {
        if (isTimerRunning == true)
        {
            // *** ช่วงที่มีการจับเวลาอยู่จะมีการบันทึกเวลาไปเก็บไว้ที่ตัวแปร myItem_time ที่ประกาศไว้ในไฟล์ GameManager ***
            //GameManager.instance.myItem_time += Time.deltaTime;
            timeElapsed += Time.deltaTime;
        }
    }

    public void ShowTutorial1()
    {
        panelTutorial1.SetActive(true);
        panelRemember.SetActive(false);
        panelTutorial2.SetActive(false);
        panelPlay.SetActive(false);
        
        if (panelNextGame != null) panelNextGame.SetActive(false); 
        // *** สั่งซ่อน UI คำชมไว้ก่อนตอนเริ่มเกม ***
        if (panelCompliment != null) panelCompliment.SetActive(false);
        
        isTimerRunning = false;
    }

    public void GoToRemember()
    {
        panelTutorial1.SetActive(false);
        panelRemember.SetActive(true);
        isTimerRunning = true; 
        GenerateRememberItems(); 
    }

    void GenerateRememberItems()
    {
        if (allItemImages.Count < 12) return;
        foreach (Transform child in gridParentRemember) { Destroy(child.gameObject); }

        rememberedItems.Clear();
        List<Sprite> tempPool = new List<Sprite>(allItemImages);

        for (int i = 0; i < 12; i++)
        {
            int randomIndex = Random.Range(0, tempPool.Count);
            rememberedItems.Add(tempPool[randomIndex]);
            tempPool.RemoveAt(randomIndex);
        }

        for (int i = 0; i < 12; i++)
        {
            GameObject newCard = Instantiate(itemPrefab, gridParentRemember);
            newCard.GetComponent<Image>().sprite = rememberedItems[i];
            newCard.GetComponent<Button>().interactable = false; 
        }
    }

    public void GoToTutorial2()
    {
        tutorialText2.text = "น่าแปลก ที่มีของบางชิ้นที่ไม่ได้มาจากกระเป๋าของคุณ เลือกมันออกไปซะ";

        panelRemember.SetActive(false);
        panelTutorial2.SetActive(true);
        isTimerRunning = false; 
    }

    public void GoToPlay()
    {
        panelTutorial2.SetActive(false);
        panelPlay.SetActive(true);
        isTimerRunning = true; 
        
        totalScore = 100; 
        foundCount = 0; 
        
        if(scoreText != null) 
        {
            scoreText.text = "Score: " + totalScore;
        }
        
        GeneratePlayItems();
    }

    void GeneratePlayItems()
    {
        foreach (Transform child in gridParentPlay) { Destroy(child.gameObject); }
        playItems.Clear();

        List<Sprite> tempRemembered = new List<Sprite>(rememberedItems);
        for (int i = 0; i < 7; i++)
        {
            int rand = Random.Range(0, tempRemembered.Count);
            playItems.Add(tempRemembered[rand]);
            tempRemembered.RemoveAt(rand);
        }

        List<Sprite> availableNewItems = new List<Sprite>();
        foreach (Sprite img in allItemImages)
        {
            if (!rememberedItems.Contains(img))
            {
                availableNewItems.Add(img);
            }
        }

        for (int i = 0; i < 5; i++)
        {
            int rand = Random.Range(0, availableNewItems.Count);
            playItems.Add(availableNewItems[rand]);
            availableNewItems.RemoveAt(rand);
        }

        for (int i = 0; i < playItems.Count; i++)
        {
            Sprite temp = playItems[i];
            int randomIndex = Random.Range(i, playItems.Count);
            playItems[i] = playItems[randomIndex];
            playItems[randomIndex] = temp;
        }

        for (int i = 0; i < playItems.Count; i++)
        {
 
            GameObject newCard = Instantiate(itemPrefab, gridParentPlay);
            newCard.GetComponent<Image>().sprite = playItems[i];
            
            Button cardButton = newCard.GetComponent<Button>();
            cardButton.interactable = true; 

            // --- สั่งให้ปุ่มรู้จักฟังก์ชัน CheckClickedItem อัตโนมัติ ---
            Sprite currentSprite = playItems[i];
            GameObject currentCard = newCard;
            
            cardButton.onClick.RemoveAllListeners();
            cardButton.onClick.AddListener(() => CheckClickedItem(currentSprite, currentCard));
        }
    }

    public void CheckClickedItem(Sprite clickedSprite, GameObject cardObject)
    {
        if (rememberedItems.Contains(clickedSprite))
        {
            // เลือกผิด! 
            totalScore--; 
            if(scoreText != null) scoreText.text = "Score: " + totalScore;

            /*CardFeedback feedback = cardObject.GetComponent<CardFeedback>();
            if (feedback != null)
            {
                feedback.ShowWrong();
            }*/
        }
        else
        {
            // เลือกถูก! 
            cardObject.GetComponent<Button>().interactable = false; 
            //cardObject.GetComponent<Image>().color = Color.green; 
            CardFeedback feedback = cardObject.GetComponent<CardFeedback>();

            if (feedback != null)
            {
                feedback.ShowCorrect();
            }
                        
            //totalScore++; 
            foundCount++; 
            
            if(scoreText != null) scoreText.text = "Score: " + totalScore;

            if (foundCount >= 5)
            {
                isTimerRunning = false; 

                // *** บันทึกคะแนนและเวลาของด่าน 1 เก็บไว้ ***
                PlayerPrefs.SetInt("Level1Score", totalScore);
                PlayerPrefs.SetFloat("Level1Time", timeElapsed); 
                PlayerPrefs.Save();
                
                //PlayerPrefs.SetFloat("Level1Time", GameManager.instance.myItem_time); 

                
                // *** เรียกใช้งาน Coroutine แทนการเปิด panelNextGame ทันที ***
                StartCoroutine(ShowComplimentAndNextPanel());
            
                return;
            }
        }

        ShuffleCardsPosition();
    }

    // สลับตำแหน่งการ์ดที่อยู่บนกระดาน
    void ShuffleCardsPosition()
    {
        int childCount = gridParentPlay.childCount;
        
        for (int i = 0; i < childCount; i++)
        {
            // ดึงการ์ดที่กำลังวนลูปอยู่
            Transform card = gridParentPlay.GetChild(i);
            
            // สุ่มตำแหน่งใหม่ (Index)
            int randomIndex = Random.Range(0, childCount);
            
            // ย้ายการ์ดไปอยู่ตำแหน่งที่สุ่มได้ (ระบบ Grid Layout จะขยับภาพบนจอให้เอง)
            card.SetSiblingIndex(randomIndex);
        }
    }

    // *** ฟังก์ชัน Coroutine จัดการแสดงคำชมและหน่วงเวลา ***
    private IEnumerator ShowComplimentAndNextPanel()
    {
        // 1. แสดงคำชม
        if (panelCompliment != null)
        {
            panelCompliment.SetActive(true);
        }

        // 2. หน่วงเวลา 3 วินาที
        yield return new WaitForSeconds(3f);

        // 3. ปิดคำชม (คุณจะปล่อยทิ้งไว้ก็ได้ ถ้าไม่ต้องการปิด) และแสดง panelNextGame
        if (panelCompliment != null)
        {
            panelCompliment.SetActive(false);
        }
        
        /*if (panelNextGame != null)
        {
            panelNextGame.SetActive(true); 
        }*/

        SceneManager.LoadScene("Scene_Level2"); 
    }

    // ปุ่ม Button_NextGame เรียก scene ใหม่
    public void GoToLevel2()
    {
        SceneManager.LoadScene("Scene_Level2"); 
    }
}