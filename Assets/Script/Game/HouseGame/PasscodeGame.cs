using UnityEngine;
using TMPro;
using System.Collections;

public class PasscodeGame : MonoBehaviour
{
    public TextMeshProUGUI codeText;
    public TextMeshProUGUI timerText;

    public StoryController storyController;

    //public TMP_InputField inputField;
    public TextMeshProUGUI resultText;
    private string playerInput = "";
    public TextMeshProUGUI inputText;

    private string currentCode;

    void OnEnable()
    {
        StartCoroutine(StartMemoryGame());
    }

    IEnumerator StartMemoryGame()
    {
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
        if (playerInput.Length < 4)
        {
            playerInput += num;
            inputText.text = playerInput;
        }
    }
    
    public void PressClear()
    {
        playerInput = "";
        inputText.text = "";
    }

    public void PressOK()
    {
        if (playerInput == currentCode)
        {
            resultText.text = "CORRECT!";
            resultText.color = Color.green;

            StartCoroutine(WaitAndGoNext()); // เปลี่ยนตรงนี้
        }
        else
        {
            resultText.text = "WRONG!";
            resultText.color = Color.red;
        }
    }

    IEnumerator WaitAndGoNext()
    {
        yield return new WaitForSeconds(1.5f);

        if (ProgressData.instance != null)
        {
            ProgressData.instance.UpdateBestScore("HouseGame", 0);
            ProgressData.instance.CompleteQuest("HouseGame", 0);
        }

        if (StreakController.instance != null)
            StreakController.instance.AddStreak();

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
