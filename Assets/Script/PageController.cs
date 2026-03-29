using UnityEngine;

public class PageController : MonoBehaviour
{
    public GameObject homeScreen;
    public GameObject questScreen;
    public GameObject gameScreen;

    void HideAll()
    {
        homeScreen.SetActive(false);
        questScreen.SetActive(false);
        gameScreen.SetActive(false);
    }

    public void ShowHome()
    {
        HideAll();
        homeScreen.SetActive(true);
    }

    public void ShowQuest()
    {
        HideAll();
        questScreen.SetActive(true);
    }

    public void ShowGame()
    {
        HideAll();
        gameScreen.SetActive(true);
    }
}

/*using UnityEngine;


public class PageController : MonoBehaviour
{
    public GameObject homeScreen;
    public GameObject chartScreen;
    public GameObject questScreen;
    public GameObject settingScreen;

    public BottomBarController bottomBar;

    void Start()
    {
        ShowHome();
    }

    void HideAll()
    {
        homeScreen.SetActive(false);
        chartScreen.SetActive(false);
        questScreen.SetActive(false);
        settingScreen.SetActive(false);
    }

    public void ShowHome()
    {
        HideAll();
        homeScreen.SetActive(true);
        bottomBar.SetActive("Home");
    }

    public void ShowChart()
    {
        HideAll();
        chartScreen.SetActive(true);
        bottomBar.SetActive("Chart");
    }

    public void ShowQuest()
    {
        HideAll();
        questScreen.SetActive(true);
        bottomBar.SetActive("Quest");
    }

    public void ShowSetting()
    {
        HideAll();
        settingScreen.SetActive(true);
        bottomBar.SetActive("Setting");
    }
}*/