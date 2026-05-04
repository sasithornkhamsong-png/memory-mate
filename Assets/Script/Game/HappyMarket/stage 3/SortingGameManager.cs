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

    private int currentSlot = 0;

    void Start()
    {
        memorizePanel.SetActive(true);
        arrangePanel.SetActive(false);
        LoadRandomRecipe();
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

            retryButton.SetActive(false);
            scoreButton.SetActive(true);
        }
        else
        {
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
}