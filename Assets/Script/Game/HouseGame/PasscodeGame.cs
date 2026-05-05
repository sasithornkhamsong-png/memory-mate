using UnityEngine;
using TMPro;
using System.Collections;

public class PasscodeGame : MonoBehaviour
{
    public TextMeshProUGUI codeText;
    public TextMeshProUGUI timerText;

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

            Invoke("GoNext", 1.5f);
        }
        else
        {
            resultText.text = "WRONG!";
            resultText.color = Color.red;
        }
    }

    void GoNext()
        {
            Debug.Log("GoNext ทำงานแล้ว!");

            ProgressData.instance.UpdateBestScore("HouseGame", GameManager.instance.score);
            ProgressData.instance.CompleteQuest("HouseGame", 0);
            StreakController.instance.AddStreak();

            gameObject.SetActive(false);
            FindObjectOfType<StoryController>().StartNextStory();
        }
}
