using UnityEngine;
using System.Collections;

/// <summary>
/// <para>This class is used for creating a singleton MonoBehaviour.</para>
/// <para>Usage:
/// class MyClass : SingletonBehaviour&lt;MyClass&gt;</para>
/// </summary>
/// <typeparam name="T"></typeparam>
public class SingletonBehaviour<T> : MonoBehaviour
    where T : SingletonBehaviour<T>
{
    private static T instance;

    /// <summary>
    /// Use this to access or create the one and only instance of the Singleton.
    /// </summary>
    public static T Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<T>();
            //instance = new GameObject(typeof(T).Name, typeof(T)).GetComponent<T>();

            return instance;
        }
    }

    /// <summary>
    /// Override this if needed, just don't forget to call it FIRST in the overriding function.
    /// </summary>
    public virtual void Awake()
    {
        //hideFlags = HideFlags.DontSave;

        //if (instance == (T)this) return;

        //if (instance != null)
        //{
        //    //DestroyImmediate(this);
        //    Debug.LogWarning("Trying to create a singleton that is already created. (" + typeof(T) + ")");
        //    Destroy(gameObject);
        //    //throw new System.InvalidOperationException("Trying to create a singleton that is already created. (" + typeof(T) + ")");
        //}

        ////DontDestroyOnLoad(this);
        //instance = (T)this;
    }

    /// <summary>
    /// Override this if needed, just don't forget to call it LAST in the overriding function.
    /// </summary>
    protected virtual void OnDestroy()
    {
        if (instance == this)
            instance = null;
    }

    /// <summary>
    /// Touch method to create the instance.
    /// </summary>
    public void Validate()
    { }
}