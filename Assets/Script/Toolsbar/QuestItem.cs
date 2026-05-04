using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QuestItem : MonoBehaviour
{
    public int reward = 1;
    public bool isDone = false;

    public Button button;
    public TMP_Text rewardText;
    
    void Start()
    {
        rewardText.text = "+" + reward;
    }

    public void StartQuest()
    {
       GameManager.instance.currentQuest = this;
       SceneManager.LoadScene("GameScene"); 
    } 

    public void CompleteQuest()
    {
        if (isDone) return;

        GameManager.instance.AddStar(reward);

        isDone = true;
        button.interactable = false;

        rewardText.text = "DONE";
    }
}