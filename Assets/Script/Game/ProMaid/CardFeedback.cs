using UnityEngine;

public class CardFeedback : MonoBehaviour
{
    public GameObject correctIcon; // ✔
    public GameObject wrongIcon;   // ❌

    public void ShowCorrect()
    {
        if (correctIcon != null)
            correctIcon.SetActive(true);
    }

    /*public void ShowWrong()
    {
        if (wrongIcon != null)
            wrongIcon.SetActive(true);
    }*/
}