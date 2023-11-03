using Lean.Touch;

using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;

using UnityEngine;

public class ViewARMenu : SimpleMenu<ViewARMenu>
{
    [SerializeField] PlaceOnPlane placeOnPlane;
    [SerializeField] TMPro.TMP_Text maleTitle;

    private GameObject loadedChracterAr;

    public void Bind(string maleName)
    {
        maleTitle.text = maleName;
        //to use male name to load AR model
        LoadARChracterPrefabFromResources(maleName);
    }

    public override void OnBackPressed()
    {

        base.OnBackPressed();
        placeOnPlane.Reset3DView();

        //do all stuff needed when exit this menu

    }
    public GameObject LoadFromResources(string objectName)
    {
        var path = "Prefabs/Models/AR/" + objectName;
        print(path);
        GameObject loadedPrefab = UnityEngine.Resources.Load<GameObject>(path);
        return loadedPrefab;
    }
    public void LoadARChracterPrefabFromResources(string objectName)
    {
        if (loadedChracterAr != null)
        {
            Destroy(loadedChracterAr);
        }

        loadedChracterAr = LoadFromResources(objectName);
        placeOnPlane.m_PlacedPrefab = loadedChracterAr;

    }
}
