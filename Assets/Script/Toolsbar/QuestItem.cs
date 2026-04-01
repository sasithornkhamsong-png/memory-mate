using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestItem : MonoBehaviour
{
    public int reward = 1;
    public bool isDone = false;

    public Button button;
    public TMP_Text rewardText;
    public GameObject gameScreen;
    public GameObject questScreen;
    public PageController pageController;

    void Start()
    {
        rewardText.text = "+" + reward + " ⭐";
    }

    public void StartQuest()
    {
        GameManager.instance.currentQuest = this;

        pageController.ShowGame();
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