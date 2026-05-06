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
    private List<Guest> selectedGuests = new List<Guest>();
    private Menu currentMenu;

    void Start()
    {
        panelInfo.SetActive(true);
        panelMenu.SetActive(false);
        panelSelect.SetActive(false);

        GenerateGuests();
        ShowGuest();
    }

    // =========================
    // แสดงแขก
    // =========================
    void GenerateGuests()
    {
        selectedGuests.Clear();

        List<Guest> temp = new List<Guest>(guests);

        for (int i = 0; i < 4; i++)
        {
            int rand = Random.Range(0, temp.Count);
            selectedGuests.Add(temp[rand]);
            temp.RemoveAt(rand);
        }
    }

    void ShowGuest()
    {
        Guest g = selectedGuests[currentIndex];

        infoText.text =
            "คุณ " + g.name +
            "\n\nชอบ: " + g.like +
            "\nแพ้/ไม่ชอบ : " + g.allergy;

        guestImage.sprite = g.image;
    }

    // =========================
    // กด Next (ตอนจำแขก)
    // =========================
    public void OnNext()
    {
        currentIndex++;

        if (currentIndex >= selectedGuests.Count)
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
        Guest g = selectedGuests[index];

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

        ProgressData.instance.UpdateBestScore("ProMaid", GameManager.instance.score);
        ProgressData.instance.CompleteQuest("ProMaid", 0);
        StreakController.instance.AddStreak();
        
        gameObject.SetActive(false);
        FindObjectOfType<ProMaidStoryController>().StartNextStory();
    }

    public Image[] guestSlots;
    public TextMeshProUGUI[] guestNames;

    void SetupSelectionUI()
    {
        for (int i = 0; i < guestSlots.Length; i++)
        {
            guestSlots[i].sprite = selectedGuests[i].image;
            guestNames[i].text = selectedGuests[i].name;
        }
    }

    public void GoToSelect()
    {
        panelInfo.SetActive(false);
        panelMenu.SetActive(false);
        panelSelect.SetActive(true);

        SetupSelectionUI();
    }

   
}