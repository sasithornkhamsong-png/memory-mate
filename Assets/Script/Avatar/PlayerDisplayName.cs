using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerSetupManager : MonoBehaviour
{
    public TMP_InputField nameInput;

    public void Continue()
    {
        string playerName = nameInput.text;

        if (playerName == "")
        {
            playerName = "ผู้เล่น";
        }

        Debug.Log("Player Name: " + playerName);

        PlayerPrefs.SetString("PlayerName", playerName);
        PlayerPrefs.Save();

        SceneManager.LoadScene("MainMenu");
    }
}