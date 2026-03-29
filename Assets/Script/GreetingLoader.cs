using UnityEngine;
using TMPro;

public class GreetingLoader : MonoBehaviour
{
    public TextMeshProUGUI greetingText;

    void Start()
    {
        string playerName = PlayerPrefs.GetString("PlayerName");
        greetingText.text = "สวัสดี " + playerName + "!";
    }
}