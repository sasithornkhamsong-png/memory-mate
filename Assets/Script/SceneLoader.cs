using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadHouseGame()
    {
        SceneManager.LoadScene("HouseGame");
    }
}