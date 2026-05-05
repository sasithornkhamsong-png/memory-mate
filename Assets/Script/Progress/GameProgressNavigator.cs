using UnityEngine;
using TMPro;

public class GameProgressNavigator : MonoBehaviour
{
    [System.Serializable]
    public class GameProgressEntry
    {
        public string gameName;
        public GameObject panel; // panel ของแต่ละเกม
    }

    public GameProgressEntry[] games;
    public TextMeshProUGUI gameNameText;

    private int currentIndex = 0;

    void Start()
    {
        ShowGame(0);
    }

    public void Next()
    {
        currentIndex = (currentIndex + 1) % games.Length;
        ShowGame(currentIndex);
    }

    public void Previous()
    {
        currentIndex = (currentIndex - 1 + games.Length) % games.Length;
        ShowGame(currentIndex);
    }

    void ShowGame(int index)
    {
        // ซ่อนทุก panel ก่อน
        foreach (var game in games)
            game.panel.SetActive(false);

        // เปิดแค่อันที่เลือก
        games[index].panel.SetActive(true);
        gameNameText.text = games[index].gameName;
    }
}