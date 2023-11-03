using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestaurantItemView : ViewElement<RestaurantModel>
{
    [Header("Data")]
    public TMPro.TMP_Text Name;
    public TMPro.TMP_Text Rate;
    public Image logo;

    void Awake()
    {
        var resturntButton = GetComponent<Button>();
        resturntButton.onClick.AddListener(OnClicked);
    }

    public override void UpdateView(RestaurantModel t)
    {
        logo.sprite = t.Logo;
        Name.text = t.RestaurantName;

        float normalSeed = 25;
        float timeFar =  (Vector3.Distance(Vector3.zero, new Vector2( t.locationOnMap.x,t.locationOnMap.y)) / 1000 / normalSeed)*60;

        Rate.text = Mathf.CeilToInt(timeFar)+ " min";
    }

}
