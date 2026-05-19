using UnityEngine;
using TMPro;
using System.Collections;

public class PasscodeGame : MonoBehaviour
{
    //public TMP_InputField inputField;
    public TextMeshProUGUI codeText;
    public TextMeshProUGUI timerText;
    public StoryController storyController;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI inputText;

    private string playerInput = "";
    private string currentCode;
    private bool isReadyToInput = false; // ในตอนแสดงตัวเลขผู้เล่นจะไม่สามารถกดแป้นพิมพ์ได้
    private int playerScore = 100;
    private float startTime;
    private bool isTiming = false;

    void OnEnable()
    {
        playerScore = 100;
        isTiming = false;
        StartCoroutine(StartMemoryGame());
    }

    IEnumerator StartMemoryGame()
{
    isReadyToInput = false;
    isTiming = false;
    GenerateCode();
    codeText.text = currentCode;

    // นับถอยหลัง 5 วิ
    for (int i = 5; i > 0; i--)
    {
        timerText.text = i.ToString();
        yield return new WaitForSeconds(1f);
    }

    // ซ่อนรหัส
    codeText.text = "????";
    timerText.text = "";
    isReadyToInput = true;

    startTime = Time.time;
    isTiming = true;
}


    void GenerateCode()
    {
        currentCode = "";

        for (int i = 0; i < 4; i++)
        {
            currentCode += Random.Range(0, 10).ToString();
        }
    }

    /*public void CheckAnswer()
    {
        string input = inputField.text.Trim(); // ตัด space หน้า-หลัง

        if (input == currentCode)
        {
            resultText.text = "CORRECT!";
            resultText.color = Color.green;
        }
        else
        {
            resultText.text = "WRONG!";
            resultText.color = Color.red;
        }
    }*/

    public void PressNumber(string num)
   {
    if (!isReadyToInput) return;

    if (playerInput.Length < 4)
    {
        playerInput += num;
        inputText.text = playerInput;
    }
   }

    public void PressClear()
{
    if (!isReadyToInput) return; 
    playerInput = "";
    inputText.text = "";
}


    public void PressOK()
    {
        if (!isReadyToInput) return;
        if (playerInput == currentCode)
        {
            isReadyToInput = false;

            if(isTiming)
            {
                isTiming = false;
                float timeUsed = Time.time - startTime;
                PlayerPrefs.SetFloat("time_Game1Level1",timeUsed);
            }
            resultText.text = "CORRECT!";
            resultText.color = Color.green;

            PlayerPrefs.SetInt("score_Game1Level1",playerScore);
            PlayerPrefs.Save();

            StartCoroutine(WaitAndGoNext()); // เปลี่ยนตรงนี้
        }
        else
        {
            resultText.text = "WRONG!";
            resultText.color = Color.red;

            playerScore--;
            if(playerScore < 0) playerScore = 0;
        }
    }

    IEnumerator WaitAndGoNext()
    {
        yield return new WaitForSeconds(1.5f);

        if (ProgressData.instance != null)
        {
            ProgressData.instance.UpdateBestScore("HouseGame", playerScore);
            ProgressData.instance.CompleteQuest("HouseGame", 0);
        }

        /*if (StreakController.instance != null)
            StreakController.instance.AddStreak();*/
            
        storyController.StartNextStory();
        gameObject.SetActive(false);
    }

    /*void GoNext()
    {
        Debug.Log("GoNext ทำงานแล้ว!");

        if (ProgressData.instance != null)
        {
            ProgressData.instance.UpdateBestScore("HouseGame", GameManager.instance.score);
            ProgressData.instance.CompleteQuest("HouseGame", 0);
        }

        if (StreakController.instance != null)
            StreakController.instance.AddStreak();

        // เรียก StoryController ก่อน แล้วค่อยปิด panel
        storyController.StartNextStory();
        gameObject.SetActive(false);
    }*/
}
