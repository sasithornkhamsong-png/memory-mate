using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class TableGame : MonoBehaviour
{
    public TextMeshProUGUI[] cellTexts;

    private List<int> originalNumbers = new List<int>();
    private List<int> newNumbers = new List<int>();
    private List<bool> isChanged = new List<bool>();
    private bool[] clicked;
    private bool canClick = false;
    public TextMeshProUGUI resultText;

    private int correctCount = 0;
    private int selectedCorrect = 0;

    void OnEnable()
    {
        resultText.text = "";

        StartCoroutine(GameFlow());
        clicked = new bool[cellTexts.Length];
    }

    IEnumerator GameFlow()
    {
        selectedCorrect = 0;
        correctCount = 0;
        canClick = false; // ❌ ห้ามกด

        GenerateOriginal();
        ShowNumbers(originalNumbers);

        yield return new WaitForSeconds(3f);

        GenerateNew();
        ShowNumbers(newNumbers);

        canClick = true; // ✔ เริ่มกดได้
    }

    void GenerateOriginal()
    {
        originalNumbers.Clear();

        for (int i = 0; i < cellTexts.Length; i++)
        {
            originalNumbers.Add(Random.Range(1, 100));
        }
    }

    void GenerateNew()
    {
        newNumbers = new List<int>(originalNumbers);
        isChanged.Clear();
        correctCount = 0;
        selectedCorrect = 0;

        for (int i = 0; i < newNumbers.Count; i++)
        {
            if (Random.value < 0.5f)
            {
                int newNum;

                do
                {
                    newNum = Random.Range(1, 100);
                }
                while (newNum == originalNumbers[i]);

                newNumbers[i] = newNum;
                isChanged.Add(true);
                correctCount++;
            }
            else
            {
                isChanged.Add(false);
            }
        }
    }

    void ShowNumbers(List<int> numbers)
    {
        for (int i = 0; i < cellTexts.Length; i++)
        {
            cellTexts[i].text = numbers[i].ToString();
        }
    }

        public void OnCellClicked(int index)
    {
        if (!canClick) return;
        //if (clicked[index]) return; // ❌ กดซ้ำไม่ทำงาน

        clicked[index] = true;

        CellButton cell = cellTexts[index].transform.parent.GetComponent<CellButton>();

        if (isChanged[index])
        {
            cell.SetColor(Color.green);
            selectedCorrect++;

            if (selectedCorrect >= correctCount)
                {
                    Debug.Log("WIN!");

                    resultText.text = "CLEAR!";
                    resultText.color = Color.green;

                    Invoke("GoNext", 1.5f);
                }
        }
            else
                {
                    cell.SetColor(Color.red);
                    Debug.Log("WRONG!");

                    resultText.text = "FAIL!";
                    resultText.color = Color.red;

                    StartCoroutine(LoseAndRestart());
                }
        }

    void GoNext()
    {
        ProgressData.instance.UpdateBestScore("HouseGame", GameManager.instance.score);
        ProgressData.instance.CompleteQuest("HouseGame", 1);
        StreakController.instance.AddStreak();
        
        gameObject.SetActive(false);
        FindObjectOfType<StoryController>().StartNextStory();
    }

        void LoseGame()
    {
        Debug.Log("LOSE!");

        // รีสตาร์ทง่าย ๆ
        gameObject.SetActive(false);
        gameObject.SetActive(true);
    }

    IEnumerator LoseAndRestart()
    {
        canClick = false;

        resultText.text = "FAIL!";
        resultText.color = Color.red;

        yield return new WaitForSeconds(2f); // ⏳ รอ 2 วิ

        ResetColors();       // 🎨 กลับเป็นสีขาว
        resultText.text = ""; // ล้างข้อความ

        StartCoroutine(GameFlow()); // 🔄 เริ่มเกมใหม่
    }

    void ResetColors()
    {
        for (int i = 0; i < cellTexts.Length; i++)
        {
            CellButton cell = cellTexts[i].transform.parent.GetComponent<CellButton>();
            cell.SetColor(Color.white);
        }
    }

}