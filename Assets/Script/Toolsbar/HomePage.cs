using UnityEngine;

public class HomePage : MonoBehaviour
{
    void Start()
    {
        BottomBarController bottomBar = FindObjectOfType<BottomBarController>();
        bottomBar.SetActive("Home");
    }
}