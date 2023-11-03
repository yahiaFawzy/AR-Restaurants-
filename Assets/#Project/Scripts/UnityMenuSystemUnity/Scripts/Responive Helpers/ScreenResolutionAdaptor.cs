using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
[ExecuteInEditMode]
#endif
public class ScreenResolutionAdaptor : MonoBehaviour
{
    private static float width = 1080;
    private static float height = 2400;

    static float refResolution = width / height;

    protected virtual void Start()
    {
        AdaptToScreenResoltion();
    }

    float scaler = 1;

#if UNITY_EDITOR
    private void Update()
    {
        AdaptToScreenResoltion();
    }
#endif

    protected void AdaptToScreenResoltion()
    {

        //calcultae canvas match
        var canvasScaler = GetComponentInParent<CanvasScaler>();
        float deviceResolution = (float)Screen.width / Screen.height;
        canvasScaler.referenceResolution = new Vector2(width, height);

        //apply canvas match
        var newScaler = deviceResolution * refResolution;
        var scalerWithLimiation = Mathf.Clamp(newScaler, 0, 1);
        canvasScaler.matchWidthOrHeight = scalerWithLimiation;

        canvasScaler.matchWidthOrHeight = 1;
    
    }

}

