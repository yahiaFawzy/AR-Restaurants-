using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Colors", fileName = "Colors Data", order = 3)]

public class ColorsData : ScriptableObject
{
    public static ColorsData _instance;


    [Header("Home Tab Layout colors")]
    [SerializeField]public Color tabSelctedColor;
    [SerializeField]public  Color tabUnSelctedColor;

    [Header("Themes")]

    [Header("Background")]
    [SerializeField] public Color _BackGroundColor;
  
    [Header("Txt")]
    [SerializeField] public Color _MainTextColor;
    [SerializeField] public Color _subtextColors;
    
    [Header("Icons")]
    [SerializeField] public Color _IconsColor;
    [SerializeField] public Color _subIconsColor;
}
