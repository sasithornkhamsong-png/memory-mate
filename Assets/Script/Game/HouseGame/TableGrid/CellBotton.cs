using UnityEngine;
using UnityEngine.UI;

public class CellButton : MonoBehaviour
{
    public int index;
    public TableGame game;
    public ShelfGame shelfGame;

    private Button btn;
    private Image img;

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
    }

    public void SetColor(Color color)
    {
        img.color = color;
    }
}