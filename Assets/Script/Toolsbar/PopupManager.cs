using UnityEngine;

public class PopupManager : MonoBehaviour
{
    public GameObject languagePopup;

    public void OpenLanguagePopup()
    {
        languagePopup.SetActive(true);
    }

    public void CloseLanguagePopup()
    {
        languagePopup.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();

        Debug.Log("Quit Game");
    }
}