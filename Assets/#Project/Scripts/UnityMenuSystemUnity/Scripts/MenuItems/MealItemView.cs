using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MealItemView : ViewElement<Meal>
{

    [Header("Data")]
    public TMPro.TMP_Text Name;
    public TMPro.TMP_Text Description;
    public TMPro.TMP_Text Price;
    public Image logo;

    void Awake()
    {
        var resturntButton = GetComponent<Button>();
        resturntButton.onClick.AddListener(OnClicked);
    }

    public override void UpdateView(Meal t)
    {
        Name.text = t.Name;
        Description.text = t.Description;
        logo.sprite =  t.logo;
        Price.text = "$" + t.Price; 
    }
}


public class MaleItemData {
   public  string logoUrl;
   public string maleName;
   public string maleDecription;
   public float price;
}