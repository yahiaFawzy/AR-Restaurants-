using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThemeAbleMenu : MonoBehaviour
{
    [Header("menu themes ui items")]
    [SerializeField] Image[] backGroundImages;
    [SerializeField] TMPro.TMP_Text[] mainTxts;
    [SerializeField] TMPro.TMP_Text[]subTxts;
    [SerializeField] Image [] icons;
    [SerializeField] Image [] subIcons;


    public virtual void OnEnable()
    {
        ApplyThemes();
    }

    /// <summary>
    /// applay color to txt and images
    /// </summary>
    protected virtual void ApplyThemes()
    {
        //  if (MenuSettings.Instance == null) return;

        var colorData = ColorsData._instance;


        Color backGroundColor = colorData._BackGroundColor;
        for (int i = 0; i < backGroundImages.Length; i++)
        {
            backGroundImages[i].color = backGroundColor;
        }

        Color mainTextColor = colorData._MainTextColor;
        for (int i = 0; i < mainTxts.Length; i++)
        {
            mainTxts[i].color = mainTextColor;
        }


        Color subTextColor = colorData._subtextColors;
        for (int i = 0; i < subTxts.Length; i++)
        {
            subTxts[i].color = subTextColor;
        }

        Color iconsColor = colorData._IconsColor;
        for (int i = 0; i < icons.Length; i++)
        {
            icons[i].color = iconsColor;
        }

        Color subIconscolor = colorData._subIconsColor;
        for (int i = 0; i < subIcons.Length; i++)
        {
            subIcons[i].color = subIconscolor;
        }

    }
}
