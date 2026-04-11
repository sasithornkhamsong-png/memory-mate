using UnityEngine;
using UnityEngine.UI;

public class CellButton : MonoBehaviour
{
    public int index;
    public TableGame game;
    public ShelfGame shelfGame;
    public PartyGame partyGame;

    private Button btn;
    private Image img;
    public GameObject checkMark;
    public GameObject wrongMark;

    void Start()
    {
        btn = GetComponent<Button>();
        img = GetComponent<Image>();

        btn.onClick.AddListener(OnClick);
    }

    void OnClick()
    {
        if (game != null)
            game.OnCellClicked(index);

        if (shelfGame != null)
            shelfGame.OnItemClicked(index);

        if (partyGame != null)
        partyGame.OnGuestSelected(index);
    }

    public void SetColor(Color color)
    {
        img.color = color;
    }

    public void ShowCorrect()
    {
        if (checkMark != null)
            checkMark.SetActive(true);
    }

    public void ShowWrong()
    {
        if (wrongMark != null)
            wrongMark.SetActive(true);
    }
}