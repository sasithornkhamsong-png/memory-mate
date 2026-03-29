using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void GoToPlayerSetup()
    {
        SceneManager.LoadScene("PlayerSetup");
    }
}


