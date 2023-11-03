using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChatMenu : SimpleMenu<ChatMenu>
{

    [SerializeField] Button backButton;
    [SerializeField] Image  mealImage;
    [SerializeField] TMPro.TMP_Text  mealName;
    [SerializeField] Canvas _3dCanvas;
    [SerializeField] Transform instantiatePlace;

    GameObject currentObject;

    protected override void IntilizeMenuCanvas()
    {
        canvas = GetComponentInChildren<Canvas>();
    }

    protected override void Start()
    {
        base.Start();
        _3dCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        _3dCanvas.worldCamera = Camera.main;
    }

    protected override void AssignActions()
    {
        backButton.onClick.AddListener(OnBackPressed);
    }

    public void Bind(Meal meal)
    {
        mealImage.sprite = meal.logo;
        mealName.text = meal.Name;

        if (currentObject != null)
        {
            Destroy(currentObject);
        }

        var path = "Prefabs/Models/3D/" + meal.Name;
        GameObject currentLoadedPrefab = ResourcesLoader.LoadFromResources(path);
        currentObject = Instantiate(currentLoadedPrefab, instantiatePlace);
        currentObject.transform.localScale *= 8000;

    }

}
