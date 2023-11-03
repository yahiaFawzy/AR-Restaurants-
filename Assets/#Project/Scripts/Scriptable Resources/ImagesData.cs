using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "Images", fileName = "Images Data", order = 2)]

public class ImagesData : ScriptableObject
{

    public static ImagesData _instance;

  
    [Header("Home Tab Layout Icons")]
    public Sprite home;
    public Sprite profile;
    public Sprite prucache;
    public Sprite message;

    [Header("Restrants Logos")]
    public Sprite HealthyFoodLogo;
    public Sprite VeganLogo;

    [Header("Males")]
    public Sprite FruitSaladMale;
}
