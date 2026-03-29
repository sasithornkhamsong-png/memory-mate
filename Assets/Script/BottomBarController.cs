using UnityEngine;
using UnityEngine.UI;

public class BottomBarController : MonoBehaviour
{
    public Image homeIcon;
    public Image chartIcon;
    public Image questIcon;
    public Image settingIcon;

    public Color activeColor = new Color(0.6f, 0.3f, 0.9f, 0.9f); // สีม่วง
    public Color inactiveColor = new Color(1f, 1f, 1f, 1f); // สีขาว

    public void SetActive(string page)
    {
        // reset ทุกอันก่อน
        homeIcon.color = inactiveColor;
        questIcon.color = inactiveColor;
        chartIcon.color = inactiveColor;
        settingIcon.color = inactiveColor;

        // เปลี่ยนเฉพาะอันที่ active
        if (page == "Home")
            homeIcon.color = activeColor;

        else if(page == "Quest")
            questIcon.color = activeColor;

        else if (page == "Chart")
            chartIcon.color = activeColor;

        else if (page == "Setting")
            settingIcon.color = activeColor;
    }
}