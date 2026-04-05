using UnityEngine;
using UnityEngine.UI;

public class CellButton : MonoBehaviour
{
    public int index;
    public TableGame game;

    private Button btn;
    private Image img;

    void Start()
    {
        btn = GetComponent<Button>();
        img = GetComponent<Image>();

        btn.onClick.AddListener(OnClick); // 🔥 สำคัญ
    }

    void OnClick()
    {
        game.OnCellClicked(index);
    }

    public void SetColor(Color color)
    {
        img.color = color;
    }
}