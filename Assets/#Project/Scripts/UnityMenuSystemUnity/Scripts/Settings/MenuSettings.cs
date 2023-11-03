using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSettings : MonoBehaviour
{
    public static MenuSettings Instance { get; private set; }


    public Color _themesColor;
    public Languages selecteLangues;

    private void Awake()
    {
        Instance = this;
    }

    public Languages Languages
    {
        get
        {

            return (Languages)PlayerPrefs.GetInt("Langues", 0);
        }
        set
        {
            selecteLangues = value;
            PlayerPrefs.SetInt("Langues", (int)selecteLangues);
        }
    }
}

public enum Languages
{
    English = 0, Arabic = 1, Turkish = 2
}

