using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public class Guest
{
    public string name;
    public string like;
    public string allergy;
    public Sprite image;
}

[System.Serializable]
public class Menu
{
    public string menuName;
    public string ingredient;
    public Sprite image;
}

public class PartyGame : MonoBehaviour
{
    [Header("UI - Info Panel")]
    public GameObject panelInfo;
    public TextMeshProUGUI infoText;
    public Image guestImage;

    [Header("UI - Menu Panel")]
    public GameObject panelMenu;
    public TextMeshProUGUI menuText;
    public Image foodImage;

    [Header("UI - Select Panel")]
    public GameObject panelSelect;

    [Header("Data")]
    public List<Guest> guests = new List<Guest>();
    public List<Menu> menus = new List<Menu>();

    private int currentIndex = 0;
    private Menu currentMenu;

    void Start()
    {
        panelInfo.SetActive(true);
        panelMenu.SetActive(false);
        panelSelect.SetActive(false);

        ShowGuest();
    }

    // =========================
    // แสดงแขก
    // =========================
    void ShowGuest()
    {
        Guest g = guests[currentIndex];

        infoText.text =
            "คุณ " + g.name +
            "\n\nชอบ: " + g.like +
            "\nแพ้: " + g.allergy;

        guestImage.sprite = g.image;
    }

    // =========================
    // กด Next (ตอนจำแขก)
    // =========================
    public void OnNext()
    {
        currentIndex++;

        if (currentIndex >= guests.Count)
        {
            GoToMenu();
            return;
        }

        ShowGuest();
    }

    // =========================
    // ไปหน้าเมนู + สุ่มเมนู
    // =========================
    void GoToMenu()
    {
        panelInfo.SetActive(false);
        panelMenu.SetActive(true);
        panelSelect.SetActive(false);

        // สุ่มเมนู
        currentMenu = menus[Random.Range(0, menus.Count)];

        menuText.text =
            currentMenu.menuName +
            "\nมีส่วนผสม: " + currentMenu.ingredient;

        foodImage.sprite = currentMenu.image;

         SetupSelectionUI();
    }

    // =========================
    // เช็คว่าแขกคนนี้กินได้ไหม
    // (ใช้ใน STEP ต่อไป)
    // =========================
    public bool CanServe(Guest g)
    {
        return g.allergy != currentMenu.ingredient;
    }

    // =========================
    // กดเลือกแขก
    // =========================
    public void OnGuestSelected(int index)
    {
        Guest g = guests[index];

        if (CanServe(g))
        {
            Debug.Log("CORRECT!");
            Invoke("WinGame", 1f);
        }
        else
        {
            Debug.Log("WRONG!");
            // ❌ ยังไม่ reset (ตามที่โบอยากได้)
        }
    }

    void WinGame()
    {
        Debug.Log("WIN!");

        // ไปด่านต่อไป
        // หรือเรียก StoryController
    }

    public Image[] guestSlots;

    void SetupSelectionUI()
    {
        for (int i = 0; i < guestSlots.Length; i++)
        {
            guestSlots[i].sprite = guests[i].image;
        }
    }

    public void GoToSelect()
    {
        panelInfo.SetActive(false);
        panelMenu.SetActive(false);
        panelSelect.SetActive(true);
    }

   
}