using UnityEngine;
using UnityEngine.SceneManagement;

public class HomeManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MainMenu"); //กลับหน้าหลัก
    }
}