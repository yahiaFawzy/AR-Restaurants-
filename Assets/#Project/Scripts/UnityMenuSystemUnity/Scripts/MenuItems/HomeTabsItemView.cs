using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HomeTabsItemView : SelectableViewElement<HomeTabsData>
{
    [Header("UI")]
    [SerializeField] Image backGroundImg;
    [SerializeField] Image mainIcon;
    [SerializeField] Image notificationIcon;
    [SerializeField] TMPro.TMP_Text mainLabel;
    [SerializeField] TMPro.TMP_Text notficationLabel;

    private Button thisButton;

    private void Awake()
    {
       thisButton = GetComponent<Button>();
       thisButton.onClick.AddListener(OnClicked);
    }

    public override void Select()
    {
        thisButton.image.rectTransform.sizeDelta = new Vector2(300,thisButton.image.rectTransform.sizeDelta.y);
        mainLabel.enabled = true;

        thisButton.image.color = ColorsData._instance.tabSelctedColor;
        
    }

    public override void UnSelect()
    {
        thisButton.image.rectTransform.sizeDelta = new Vector2(110,thisButton.image.rectTransform.sizeDelta.y);      
        mainLabel.enabled = false;
        thisButton.image.color = ColorsData._instance.tabUnSelctedColor;
    }

 

    public override void UpdateView(HomeTabsData t)
    {

        //update views 
        this.mainIcon.sprite = t.mainIcon;
        this.mainLabel.text = t.mainLabel;

        /*if (t.notficationLabel != null)
        {
            this.notficationLabel.text = t.notficationLabel; 
            this.notficationLabel.gameObject.SetActive(true);          
            this.notificationIcon.gameObject.SetActive(true);          
        }
        else {
            this.notificationIcon.gameObject.SetActive(false);          
            this.notficationLabel.gameObject.SetActive(false);          
        }*/

    }


}


[SerializeField]
public class HomeTabsData {
    public Sprite mainIcon;
    public string mainLabel;
    public string notficationLabel;
}

