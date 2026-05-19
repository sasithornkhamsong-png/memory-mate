using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerSetupManager : MonoBehaviour
{
    public TMP_InputField nameInput;

    void Start()
    {
        // เช็คตอนเปิดหน้า Setup
        string savedName = PlayerPrefs.GetString("PlayerName", "");
        if (savedName != "")
        {
            // มีชื่อแล้ว - ข้ามไป MainMenu เลย
            SceneManager.LoadScene("MainMenu");
        }
    }

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