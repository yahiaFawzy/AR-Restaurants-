using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesLoader 
{
    public static GameObject LoadFromResources(string path)
    {
        GameObject loadedPrefab = UnityEngine.Resources.Load<GameObject>(path);
        return loadedPrefab;
    }
}
