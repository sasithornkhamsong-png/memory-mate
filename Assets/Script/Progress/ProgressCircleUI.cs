using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ProgressCircleUI : MonoBehaviour
{
    public Image circle;
    public TextMeshProUGUI percentText;

    public void SetProgress()
    {
        float progress = ProgressData.instance.GetProgress();

        circle.fillAmount = progress;
        percentText.text = Mathf.RoundToInt(progress * 100) + "%";
    }

    void Start()
    {
        SetProgress();
    }
}