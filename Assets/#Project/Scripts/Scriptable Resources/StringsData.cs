using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Strings",fileName ="Lables",order =1)]
public class StringsData : ScriptableObject
{

    //instance
    public static StringsData _instance;


    //data
    public string _AppName;

    [Header("Home Tab Layout strings")]
    public string home;
    public string profile;
    public string prucache;
    public string message;

}
