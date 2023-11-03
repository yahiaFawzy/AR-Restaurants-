using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewThreeDMenu : SimpleMenu<ViewThreeDMenu>
{
    [SerializeField] GameObject instantiatePlace;

    private GameObject mainCameraRef;
    private GameObject currentObject;

    [SerializeField] TMPro.TMP_Text maleTitle;


    public void Bind(string maleName) {
        maleTitle.text = maleName;
        //to use male name to load 3d model
        LoadFromResources(maleName);
    }

    public override void OnBackPressed()
    {
        if (mainCameraRef != null)
            mainCameraRef.SetActive(true);
        base.OnBackPressed();

        //do all stuff needed when exit this menu

    }

   

    public void LoadFromResources(string objectName)
    {

        if (currentObject != null)
        {
            Destroy(currentObject);
        }

        var path = "Prefabs/Models/3D/" + objectName;
        GameObject currentLoadedPrefab = ResourcesLoader.LoadFromResources(path);
        currentObject = Instantiate(currentLoadedPrefab, instantiatePlace.transform);
        mainCameraRef = Camera.main.gameObject;
        mainCameraRef.SetActive(false);
    }
}
