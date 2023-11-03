using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Server", fileName = "Server", order = 4)]

public class Server : ScriptableObject{

    public static Server _instance;
    public List<RestaurantModel> restaurantModels;

#if UNITY_EDITOR
    private void OnEnable()
    {
        //edit data in editor from code
        foreach (RestaurantModel restaurantModel in restaurantModels)
        {
            var meals = restaurantModel.meals;
            int constRang = 50;
            foreach (Meal meal in meals)
            {
                meal.healthyData.calories = (short)Random.Range(0, constRang);
                meal.healthyData.carbohydrates = (short)Random.Range(0, constRang);
                meal.healthyData.fat = (short)Random.Range(0, constRang);
                meal.healthyData.protein = (short)Random.Range(0, constRang);
                meal.healthyData.sugar = (short)Random.Range(0, constRang);
            }
        }
        //save assest 
        EditorUtility.SetDirty(this);
    }
#endif

}
