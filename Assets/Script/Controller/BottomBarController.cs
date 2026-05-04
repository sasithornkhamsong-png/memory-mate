using UnityEngine;
using UnityEngine.UI;

public class BottomBarController : MonoBehaviour
{
    public Image homeIcon;
    public Image chartIcon;
    public Image questIcon;
    public Image settingIcon;

    public Color activeColor = new Color(1f, 0.85f, 0f, 1f); // สีเหลือง 
    public Color inactiveColor = new Color(1f, 1f, 1f, 1f); // สีขาว

    public void SetActive(string page)
    {

        Debug.Log("SetActive called: " + page);
        
        homeIcon.color = inactiveColor;
        questIcon.color = inactiveColor;
        chartIcon.color = inactiveColor;
        settingIcon.color = inactiveColor;

        if (page == "Home")
            homeIcon.color = activeColor;
        else if (page == "Quest")
            questIcon.color = activeColor;
        else if (page == "Chart")
            chartIcon.color = activeColor;
        else if (page == "Setting")
            settingIcon.color = activeColor;
    }
    
}