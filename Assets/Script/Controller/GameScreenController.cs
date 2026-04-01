using UnityEngine;

public class GameScreenController : MonoBehaviour
{
    public GameObject gameScreen;
    public GameObject questScreen;
    public PageController pageController;

    public void CompleteGame()
    {
        var quest = GameManager.instance.currentQuest;

        if (quest != null)
        {
            quest.CompleteQuest();
        }

        pageController.ShowQuest();
    }
}
