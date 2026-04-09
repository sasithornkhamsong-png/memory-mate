using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

[System.Serializable]
public class Guest
{
    public string name;
    public string like;
    public string allergy;
    public Sprite image;
}

public class PartyGame : MonoBehaviour
{
    public TextMeshProUGUI infoText;
    public Image guestImage;

    public List<Guest> guests = new List<Guest>();

    private int currentIndex = 0;

    void Start()
    {
        ShowGuest();
    }

    void ShowGuest()
    {
        Guest g = guests[currentIndex];

        infoText.text =
            "คุณ " + g.name +
            "\n\nชอบ: " + g.like +
            "\nแพ้: " + g.allergy;

        guestImage.sprite = g.image;
    }

    public void OnNext()
    {
        currentIndex++;

        if (currentIndex >= guests.Count)
        {
            Debug.Log("จำครบแล้ว!");
            return;
        }

        ShowGuest();
    }
}
