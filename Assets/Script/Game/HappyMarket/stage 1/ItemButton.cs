using TMPro;
using UnityEngine;

public class ItemButton : MonoBehaviour
{
    public string itemName;
    public TextMeshProUGUI textUI;
    public HappyMarketGame game;

    public void ClickItem()
    {
        game.OnItemClicked(itemName, textUI);
    }
}