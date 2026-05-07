using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PageController : MonoBehaviour
{
    public GameObject homeScreen;
    public GameObject chartScreen;
    public GameObject questScreen;

    public Image topBarImage;
    public BottomBarController bottomBar;

    public Color homeColor;
    public Color normalColor;

    void HideAll()
    {
        if (homeScreen != null) homeScreen.SetActive(false);
        if (questScreen != null) questScreen.SetActive(false);
        if (chartScreen != null) chartScreen.SetActive(false);
    }

    void Start()
    {
        ShowHome();
    }

    public void ShowHome()
    {
        HideAll();
        homeScreen.SetActive(true);

        topBarImage.color = homeColor; //  เหลือง
         bottomBar.SetActive("Home");
    }

    public void ShowQuest()
    {
        HideAll();
        questScreen.SetActive(true);

        topBarImage.color = normalColor; //  ขาว
        bottomBar.SetActive("Quest");
    }

    public void ShowChart()
    {
        HideAll();
        chartScreen.SetActive(true);

        topBarImage.color = normalColor;
        bottomBar.SetActive("Chart");
    }

    public void ShowGame()
    {
        SceneManager.LoadScene("GameScene");
    }

    void Start()
    {
        ShowHome();
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