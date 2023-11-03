using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeMenu : SimpleMenu<HomeMenu>
{

    [Header("Restrants")]
    [SerializeField] RestaurantItemView restaurant;
    [SerializeField] RectTransform listRoot;

    [Header("Tab Layout")]
    [SerializeField] HomeTabsItemView homeTabsItemView;
    [SerializeField] RectTransform tabListRoot;


    public void OnHomeTabClicked(int index)
    {
        Debug.Log("Tabs clicked : " + index);
    }

    void OnRestrantClicked(int index)
    {
        RestaurantMenu restrantMenu = (RestaurantMenu)RestaurantMenu.Show();
        restrantMenu.Bind(Server._instance.restaurantModels[index]);
    }

    protected override void Start()
    {
        base.Start();

        List<RestaurantModel> restaurants = Server._instance.restaurantModels;
        ListClickedListener listClickedListener = new ListClickedListener(OnRestrantClicked);
       
        //adapter
        ListAdapter<RestaurantModel> listAdapter = new ListAdapter<RestaurantModel>(restaurant, listRoot, restaurants, listClickedListener);

        listAdapter.CreateViews();


        //adding tab layout
        //data 
        List<HomeTabsData> homeTabsDatas = new List<HomeTabsData>();
        homeTabsDatas.Add(new HomeTabsData() { mainIcon = ImagesData._instance.home, mainLabel = StringsData._instance.home });
        homeTabsDatas.Add(new HomeTabsData() { mainIcon = ImagesData._instance.profile, mainLabel = StringsData._instance.profile });
        homeTabsDatas.Add(new HomeTabsData() { mainIcon = ImagesData._instance.prucache, mainLabel = StringsData._instance.prucache });
        homeTabsDatas.Add(new HomeTabsData() { mainIcon = ImagesData._instance.message, mainLabel = StringsData._instance.message });
        //action 
        ListClickedListener listClickedListener2 = new ListClickedListener(OnHomeTabClicked);

        //adapter
        TabLayoutAdapter<HomeTabsData> tabLayoutAdapter = new TabLayoutAdapter<HomeTabsData>
            (homeTabsItemView, tabListRoot, homeTabsDatas, listClickedListener2);

        tabLayoutAdapter.CreateViews();
    }

    #region Data
    public class RestrantItemData
    {
        public string name;
        public float timeFar;
        internal Sprite icon;
    }



    #endregion
}
