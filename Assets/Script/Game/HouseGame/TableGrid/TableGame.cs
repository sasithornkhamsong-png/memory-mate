using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class TableGame : MonoBehaviour
{
    public TextMeshProUGUI[] cellTexts;
    public TextMeshProUGUI resultText;

    private List<int> originalNumbers = new List<int>();
    private List<int> newNumbers = new List<int>();
    private List<bool> isChanged = new List<bool>();
    private bool[] clicked;
    private bool canClick = false;
    private int correctCount = 0;
    private int selectedCorrect = 0;
    private float startTime;
    private bool isTiming = false;
    private int playerScore = 100;

    void OnEnable()
    {
        playerScore = 100;
        resultText.text = "";
        StartCoroutine(GameFlow());
    }

    IEnumerator GameFlow()
    {
        selectedCorrect = 0;
        correctCount = 0;
        canClick = false; 

        clicked = new bool[cellTexts.Length];

        GenerateOriginal();
        ShowNumbers(originalNumbers);

        yield return new WaitForSeconds(3f);

        GenerateNew();
        ShowNumbers(newNumbers);

        canClick = true; 
        startTime = Time.time;
        isTiming = true;
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
        if (clicked[index]) return; // ✔️ เปิดใช้งาน: ป้องกันการกดช่องเดิมซ้ำ

        clicked[index] = true;

        CellButton cell = cellTexts[index].transform.parent.GetComponent<CellButton>();

        if (isChanged[index])
        {
            cell.SetColor(Color.green);
            selectedCorrect++;

            if (selectedCorrect >= correctCount)
            {
                if(isTiming)
                {
                    isTiming = false;
                    float timeUsed = Time.time - startTime;
                    PlayerPrefs.SetFloat("time_Game1Level2", timeUsed);
                }
                
                PlayerPrefs.SetInt("score_Game1Level2", playerScore);
                PlayerPrefs.Save();

                Debug.Log("WIN!");
                resultText.text = "CLEAR!";
                resultText.color = Color.green;
                canClick = false; // หยุดการกดหลังจากชนะ

                Invoke("GoNext", 1.5f);
            }
        }
        else
        {
            playerScore--;
            if (playerScore < 0) playerScore = 0;
            
            cell.SetColor(Color.red);
            Debug.Log("WRONG!");
            resultText.text = "FAIL!";
            resultText.color = Color.red;

            StartCoroutine(LoseAndRestart());
        }
    }

    void GoNext()
    {
        /*if (StreakController.instance != null)
        {
            StreakController.instance.AddStreak();
        }*/
        SceneManager.LoadScene("FortuneSticks");
    }

    IEnumerator LoseAndRestart()
    {
        canClick = false;
        yield return new WaitForSeconds(2f); 

        ResetColors();       
        resultText.text = ""; 
        StartCoroutine(GameFlow()); 
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