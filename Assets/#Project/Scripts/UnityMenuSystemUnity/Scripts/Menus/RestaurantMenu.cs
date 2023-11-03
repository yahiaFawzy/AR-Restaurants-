using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestaurantMenu : SimpleMenu<RestaurantMenu>
{
    [Header("Meal")]
    [SerializeField] MealItemView mealPrefab;
    [SerializeField] RectTransform listRoot;


    [Header("Meale")]
    [SerializeField] TMPro.TMP_Text restaurant_name;
    [SerializeField] TMPro.TMP_Text restaurant_discription;
    [SerializeField] TMPro.TMP_Text restaurant_rate;
    [SerializeField] TMPro.TMP_Text restaurant_distance;
    [SerializeField] Image restaurant_background; 

    RestaurantModel restaurantModel;
    internal void Bind(RestaurantModel restaurantModel)
    {
        this.restaurantModel = restaurantModel;

        restaurant_name.text = restaurantModel.RestaurantName;
        restaurant_discription.text = restaurantModel.Description;
        restaurant_rate.text = restaurantModel.Rate+"";
        //restrant_logo.sprite = restrantModel.Logo;
        restaurant_background.sprite = restaurantModel.BackGroundImage;

        float distance = (Vector3.Distance(Vector3.zero, new Vector2(restaurantModel.locationOnMap.x, restaurantModel.locationOnMap.y)))/1000;

        restaurant_distance.text = distance.ToString("0.0")+"km";

        //action
        ListClickedListener listClickedListener = new ListClickedListener(OnMaleClicked);
        //adapter
        ListAdapter<Meal> listAdapter = new ListAdapter<Meal>(mealPrefab, listRoot, restaurantModel.meals, listClickedListener);

        listAdapter.CreateViews();
    }

    private void OnMaleClicked(int index)
    {
        MealViewMenu maleViewMenu = (MealViewMenu)MealViewMenu.Show();
        maleViewMenu.Bind(restaurantModel.meals[index]);
    }
}
