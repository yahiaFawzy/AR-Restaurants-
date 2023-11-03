using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//bind all resources 
[DefaultExecutionOrder(-100)]
public class ScriptableAssestBinder : MonoBehaviour
{
    [Header("Langues Pack")]
    public  StringsData ArabicStrings;
    public  StringsData EnglishStrings;
    public  ImagesData imagesInstance;
    public  ColorsData colorsInstance;
    public  Server server;

    private void Awake()
    {

        //to do applay diffrent langues here 
        StringsData._instance = EnglishStrings;
        ImagesData._instance = imagesInstance;
        ColorsData._instance = colorsInstance;
        Server._instance = server;
    }

}
