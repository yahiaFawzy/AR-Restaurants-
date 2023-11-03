using System;
using UnityEngine;

public abstract class Menu<T> : Menu where T : Menu<T>
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        Instance = (T)this;
		IntilizeMenuCanvas();
	}

  	protected virtual void IntilizeMenuCanvas() {
		canvas = GetComponent<Canvas>();
	}

	protected virtual void OnDestroy()
    {
        Instance = null;
	}

	protected static Menu Open()
	{
		if (Instance == null)
			MenuManager.Instance.CreateInstance<T>();
		else
			Instance.gameObject.SetActive(true);
		if (Instance)
		{
			MenuManager.Instance.OpenMenu(Instance);
		}
		else {
			Debug.Log("Instcance is null here");
		}
		return Instance;
	}

	protected static Menu OpenFirst()
	{
		if (Instance == null)
			MenuManager.Instance.CreateInstance<T>();
		else
			Instance.gameObject.SetActive(true);

		MenuManager.Instance.OpenMenuFirst(Instance);
		return Instance;
	}

	protected static void Close()
	{
		if (Instance == null)
		{
			Debug.LogErrorFormat("Trying to close menu {0} but Instance is null", typeof(T));
			return;
		}

		MenuManager.Instance.CloseMenu(Instance);
	}

	public override void OnBackPressed()
	{
		Close();
	}
}

public abstract class Menu : ThemeAbleMenu
{
	[Tooltip("Destroy the Game Object when menu is closed (reduces memory usage)")]
	public bool DestroyWhenClosed = true;

	[Tooltip("Disable menus that are under this one in the stack")]
	public bool DisableMenusUnderneath = true;

	public bool KeepINStack = true;

	internal Canvas canvas;

	protected virtual void Start()
    {
		AssignActions();
    }




	public abstract void OnBackPressed();
	protected virtual void AssignActions()
	{

	}

   
}
