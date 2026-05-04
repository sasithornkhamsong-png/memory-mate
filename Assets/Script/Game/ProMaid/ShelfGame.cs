using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ShelfGame : MonoBehaviour
{
    public Image[] itemIcons;     // ช่องบนจอ
    public Sprite[] itemSprites;  // คลังรูปทั้งหมด

    private List<int> original = new List<int>();
    private List<int> current = new List<int>();
    private List<bool> isChanged = new List<bool>();

    private bool[] clicked;

    private int correctCount = 0;
    private int selectedCorrect = 0;
    private int wrongCount = 0; // เก็บจำนวนผิด

    void OnEnable()
    {
        clicked = new bool[itemIcons.Length];
        StartCoroutine(GameFlow());
    }

    IEnumerator GameFlow()
    {
        GenerateOriginal();
        ShowItems(original);

        yield return new WaitForSeconds(3f); // ให้จำ

        GenerateNew();
        ShowItems(current);
    }

    void GenerateOriginal()
    {
        original.Clear();

        for (int i = 0; i < itemIcons.Length; i++)
        {
            int rand = Random.Range(0, itemSprites.Length);
            original.Add(rand);
        }
    }

    void GenerateNew()
    {
        current = new List<int>(original);
        isChanged.Clear();

        correctCount = 0;
        selectedCorrect = 0;

        for (int i = 0; i < current.Count; i++)
        {
            if (Random.value < 0.5f)
            {
                int newItem;

                do
                {
                    newItem = Random.Range(0, itemSprites.Length);
                }
                while (newItem == original[i]);

                current[i] = newItem;
                isChanged.Add(true);
                correctCount++;
            }
            else
            {
                isChanged.Add(false);
            }
        }
    }

    void ShowItems(List<int> list)
    {
        for (int i = 0; i < itemIcons.Length; i++)
        {
            itemIcons[i].gameObject.SetActive(true); // รีเซ็ตให้กลับมาแสดง
            itemIcons[i].sprite = itemSprites[list[i]];
        }
    }

    public void OnItemClicked(int index)
    {
        if (index >= itemIcons.Length) return;
        if (clicked[index]) return;

        clicked[index] = true;

        CellButton cell = itemIcons[index].GetComponentInParent<CellButton>();

        if (isChanged[index])
        {
            // กดถูก
            //cell.SetColor(Color.green);
            cell.ShowCorrect(); 

            
            itemIcons[index].gameObject.SetActive(false);

            selectedCorrect++;

            if (selectedCorrect >= correctCount)
            {
                Debug.Log("WIN!");
                Invoke("GoNext", 1.5f);
            }
        }
        else
        {
            // กดผิด
            //cell.SetColor(Color.red);
            cell.ShowWrong();

            wrongCount++;
            Debug.Log("WRONG: " + wrongCount);
        }
    }

    void GoNext()
    {
        ProgressData.instance.UpdateBestScore("ProMaid", GameManager.instance.score);
        ProgressData.instance.CompleteQuest("ProMaid", 1);
        
        gameObject.SetActive(false);
        FindObjectOfType<ProMaidStoryController>().StartNextStory();
    }
}