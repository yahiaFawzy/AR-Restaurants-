using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1000)]
public class MenuManager : MonoBehaviour
{



    [Header("Menus Data")]
    public HomeMenu homeMenu;
    public RestaurantMenu restaurantMenu;
    public MealViewMenu mealViewMenu;
    public ViewThreeDMenu viewThreeDMenu;
    public ViewARMenu viewARMenu;
    public ChatMenu chatMenu;

     


    private Stack<Menu> menuStack = new Stack<Menu>();

 
    public static MenuManager Instance { get; set; }

   
    private void Awake()
    {
        Instance = this;
    }


 

    


    void Start() {
        // SplashScreenMenu.Show();
        HomeMenu.Show();
    }

    private void OnDestroy()
    {
        Instance = null;
    }

    public void CreateInstance<T>() where T : Menu
    {
       var prefab = GetPrefab<T>();


        Menu menu = Instantiate(prefab, transform);
        //make sure menu is active
        menu.gameObject.SetActive(true);     
    }

    public void OpenMenu(Menu instance)
    {     
        // De-activate top menu
        if (menuStack.Count > 0)
        {
            var topMenu = menuStack.Peek();
            if (!topMenu.KeepINStack)
            {
                CloseTopMenu();
            }

            if (instance.DisableMenusUnderneath)
            {
                foreach (var menu in menuStack)
                {
                    menu.gameObject.SetActive(false);

                    if (menu.DisableMenusUnderneath)
                        break;
                }
            }
            

            var topCanvas = instance.canvas;
            if (topCanvas)
            {
                var sortorder = 5;
                if (menuStack.Count > 0)
                {
                    var previousCanvas = menuStack.Peek().canvas;

                    if (previousCanvas)
                    {
                        previousCanvas.GetComponent<GraphicRaycaster>().enabled = false;
                        sortorder = previousCanvas.sortingOrder + 1;
                    }
                }

                topCanvas.sortingOrder = sortorder;
                var raycaster = topCanvas.GetComponent<GraphicRaycaster>();

                if (raycaster)
                raycaster.enabled = true;
            }           
        }
        instance.gameObject.SetActive(true);
        menuStack.Push(instance);
    }

    internal void OpenMenuFirst<T>(T instance) where T : Menu<T>
    {
        while (menuStack.Count > 0)
        {
            CloseTopMenu();
        }

        OpenMenu(instance);
    }


    private T GetPrefab<T>() where T : Menu
    {
        // Get prefab dynamically, based on public fields set from Unity
        // You can use private fields with SerializeField attribute too
        var fields = this.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        foreach (var field in fields)
        {
            var prefab = field.GetValue(this) as T;
            if (prefab != null)
            {
                return prefab;
            }
        }
        throw new MissingReferenceException("Prefab not found for type " + typeof(T));
    }

    public void CloseMenu(Menu menu)
    {
        if (menuStack.Count == 0)
        {
            Debug.LogErrorFormat(menu, "{0} cannot be closed because menu stack is empty", menu.GetType());
            return;
        }

        if (menuStack.Peek() != menu)
        {
            Debug.LogErrorFormat(menu, "{0} cannot be closed because it is not on top of stack", menu.GetType());
            return;
        }

        CloseTopMenu();
    }

    public void CloseTopMenu()
    {
        var instance = menuStack.Pop();

        if (instance.DestroyWhenClosed)
            Destroy(instance.gameObject);
        else
            instance.gameObject.SetActive(false);

        // Re-activate top menu
        // If a re-activated menu is an overlay we need to activate the menu under it
        foreach (var menu in menuStack)
        {
            menu.gameObject.SetActive(true);
            var raycaster = menu.GetComponent<GraphicRaycaster>();
            if (raycaster)
             raycaster.enabled = true;

            if (menu.DisableMenusUnderneath)
                break;
        }
    }


    private void Update()
    {
        // On Android the back button is sent as Esc
        if (Input.GetKeyDown(KeyCode.Escape) && menuStack.Count > 1)
        {
            menuStack.Peek().OnBackPressed();
        }
    }
}

