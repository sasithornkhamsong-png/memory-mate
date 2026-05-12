using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class RecipeData
{
    public string recipeName;
    public string[] steps;
}

public class SortingGameManager : MonoBehaviour
{
    public GameObject memorizePanel;
    public GameObject arrangePanel;
        
    public Button[] optionButtons;
    public TMP_Text[] answerSlots;
    public string[] correctOrder;
    
    public TMP_Text recipeNameText;
    public RecipeData[] recipes;
    public TMP_Text[] memorizeStepTexts;

    // หน้า Result
    public Image resultImage;
    public Sprite correctSprite;
    public Sprite wrongSprite;
    public GameObject retryButton;
    public GameObject scoreButton;

    public GameObject resultPanel;
    public TMP_Text resultText;

    public GameObject summaryPanel;

    [Header("Score")]
    public TMP_Text scoreText;

    private int totalScore = 100;
    private int penaltyScore = 0;

    [Header("Timer")]
    private float timeElapsed = 0f;
    private bool isTimerRunning = true;

    [Header("Final Score")]
    public TMP_Text finalScoreText;
    public GameObject panelScore;
    public TMP_Text finalTimeText;

    private int currentSlot = 0;

    void Start()
    {
        totalScore = 100;
        scoreText.text = "Score : " + totalScore;
        
        memorizePanel.SetActive(true);
        arrangePanel.SetActive(false);
        LoadRandomRecipe();
    }

    void Update()
    {
        if (isTimerRunning)
        {
            timeElapsed += Time.deltaTime;
        }
    }

    void LoadRandomRecipe()
    {
        int randomIndex = Random.Range(0, recipes.Length);
        RecipeData selectedRecipe = recipes[randomIndex];

        recipeNameText.text = selectedRecipe.recipeName;

        for (int i = 0; i < memorizeStepTexts.Length; i++)
        {
            memorizeStepTexts[i].text = selectedRecipe.steps[i];
        }
        
        correctOrder = selectedRecipe.steps;

        List<string> shuffledSteps = new List<string>(selectedRecipe.steps);

        for (int i = 0; i < shuffledSteps.Count; i++)
        {
            string temp = shuffledSteps[i];
            int random = Random.Range(i, shuffledSteps.Count);
            shuffledSteps[i] = shuffledSteps[random];
            shuffledSteps[random] = temp;
        }

        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].GetComponentInChildren<TMP_Text>().text = shuffledSteps[i];
            optionButtons[i].interactable = true;
        }

        currentSlot = 0;

        for (int i = 0; i < answerSlots.Length; i++)
        {
            answerSlots[i].text = $"ขั้นตอนที่ {i + 1}";
        }
    }

    public void SelectOption(int buttonIndex)
    {
        if (currentSlot >= answerSlots.Length)
            return;

        Button clickedButton = optionButtons[buttonIndex];
        TMP_Text buttonText = clickedButton.GetComponentInChildren<TMP_Text>();

        answerSlots[currentSlot].text = buttonText.text;
        clickedButton.interactable = false;

        currentSlot++;
    }

    public void ShowArrangePanel()
    {
        memorizePanel.SetActive(false);
        arrangePanel.SetActive(true);
    }

   public void CheckAnswer()
    {
        bool isCorrect = true;

        for (int i = 0; i < answerSlots.Length; i++)
        {
            if (answerSlots[i].text != correctOrder[i])
            {
                isCorrect = false;
                break;
            }
        }

        resultPanel.SetActive(true);

        if (isCorrect)
        {
            resultImage.sprite = correctSprite;
            resultText.text = "มื้อนี้สมบูรณ์แบบมาก !";
            PlayerPrefs.SetInt("SortingGameScore", totalScore);
            PlayerPrefs.Save();

            retryButton.SetActive(false);
            scoreButton.SetActive(true);
            //Invoke(nameof(ShowFinalScore), 3f);
        }
        else
        {
            penaltyScore++;

            totalScore = 100 - penaltyScore;

            if (totalScore < 0)
                totalScore = 0;

            scoreText.text = "Score : " + totalScore;
            resultImage.sprite = wrongSprite;
            resultText.text = "มื้อนี้ยังมีบางอย่างไม่ถูกต้อง\nลองใหม่อีกครั้งนะ";

            retryButton.SetActive(true);
            scoreButton.SetActive(false);
        }
    }

    public void Retry()
    {
        resultPanel.SetActive(false);

        currentSlot = 0;

        for (int i = 0; i < answerSlots.Length; i++)
        {
            answerSlots[i].text = $"ขั้นตอนที่ {i + 1}";
        }

        for (int i = 0; i < optionButtons.Length; i++)
        {
            optionButtons[i].interactable = true;
        }
    }

    public void GoToSummary()
    {
        //resultPanel.SetActive(false);
        arrangePanel.SetActive(false);
        //summaryPanel.SetActive(true);
    }

    public void ShowFinalScore()
    {
        isTimerRunning = false;
        PlayerPrefs.SetFloat("SortingGameTime", timeElapsed);
        PlayerPrefs.Save();
        resultPanel.SetActive(false);
        arrangePanel.SetActive(false);

        int happyMarketScore =
            PlayerPrefs.GetInt("HappyMarketScore", 0);

        int shoppingGameScore =
            PlayerPrefs.GetInt("ShoppingGameScore", 0);

        int sortingGameScore =
            PlayerPrefs.GetInt("SortingGameScore", 0);

        int finalScore =
            happyMarketScore +
            shoppingGameScore +
            sortingGameScore;

        float happyMarketTime =
            PlayerPrefs.GetFloat("HappyMarketTime", 0f);

        float shoppingGameTime =
            PlayerPrefs.GetFloat("ShoppingGameTime", 0f);

        float finalTime =
            happyMarketTime +
            shoppingGameTime +
            timeElapsed;

        finalScoreText.text = "" + finalScore;
        ProgressData.instance.SaveGameResult(
        "HappyMarket",
        finalScore,
        finalTime
    );
        panelScore.SetActive(true);

        int minutes = Mathf.FloorToInt(finalTime / 60);
        int seconds = Mathf.FloorToInt(finalTime % 60);

        finalTimeText.text =
        minutes + " m " + seconds + " s";

        int currentPlay = PlayerPrefs.GetInt("Game3_PlayCount",0);
        PlayerPrefs.SetInt("Game3_PlayCount",currentPlay+1);
        
        scoreGame3Manager.SaveEntry(finalScore, finalTime);
        PlayerPrefs.Save();

        ProgressData.instance.SaveGameResult(
        "HappyMarket",
        finalScore,
        finalTime
    );
    }
}