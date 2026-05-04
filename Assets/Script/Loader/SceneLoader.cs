using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadHouseGame()
    {
        SceneManager.LoadScene("HouseGame");
    }

    public void LoadProMaid()
    {
        SceneManager.LoadScene("ProMaid");
    } 

    public void LoadHappyMarket()
    {
        SceneManager.LoadScene("HappyMarket");
    }
}