using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class RestaurantModel 
{
    public string Id;
    public string RestaurantName;
    public Sprite Logo;
    public Sprite BackGroundImage;
    public string Description;
    public Location locationOnMap; 
    public float Rate;
    public List<Meal> meals;

  
}




[System.Serializable]
public class Meal {
    public string Id;
    public string Name;
    public string Description;
    public float Price;
    public Sprite logo;
    public Sprite bigImg;
    public List<string> components;
    public MealHealthyData healthyData;

    internal string GetHealthData()
    {
        string healthyDataTxt = "";
        int value = 15;
        if (healthyData.fat > value) {
            healthyDataTxt += "fat : "+healthyData.fat+"g\n";
        }

        if (healthyData.protein > value)
        {
            healthyDataTxt += "protein : " + healthyData.protein + "g\n";
        }

        if (healthyData.sugar > value)
        {
            healthyDataTxt += "sugar : " + healthyData.sugar + "g\n";
        }

        if (healthyData.carbohydrates > value)
        {
            healthyDataTxt += "carbohydrates : " + healthyData.carbohydrates + "g\n";
        }

        if (healthyData.calories > value)
        {
            healthyDataTxt += "calories : " + healthyData.calories + "g\n";
        }

        return healthyDataTxt;
    }
}

[Serializable]
public class MealHealthyData {
    public short fat;
    public short sugar;
    public short protein;
    public short carbohydrates;
    public short calories; 
}

[System.Serializable]
public class Location {
   public float x;
   public float y;
}