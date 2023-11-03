using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class MealViewMenu : SimpleMenu<MealViewMenu>
{
    [SerializeField] TMPro.TMP_Text mealName;
    [SerializeField] TMPro.TMP_Text mealComponent;
    [SerializeField] TMPro.TMP_Text mealDiscription;
    [SerializeField] TMPro.TMP_Text mealHeathyData;

    [SerializeField] Image mealIcon;
    [SerializeField] Button playARBtn;
    [SerializeField] Button play3DBtn;

    [SerializeField] Button startChatButton;
    [SerializeField] bool chatButtonStatus = false;
    [SerializeField] Animator flagsButtonAnimation;

    [Header("Flags Buttons")]
    [SerializeField] Button suadiButton, turkyButton, englandButton;



    Meal meal;





    protected override void AssignActions()
    {
        base.AssignActions();
        playARBtn.onClick.AddListener(() => StartArView());
        play3DBtn.onClick.AddListener(() => Start3DView());
        startChatButton.onClick.AddListener(() => OnStartChatClicked());

        //add flags clicked
        suadiButton.onClick.AddListener(() =>
        {
            MenuSettings.Instance.Languages = Languages.Arabic;
            StartChat();
        });
        englandButton.onClick.AddListener(() =>
        {
            MenuSettings.Instance.Languages = Languages.English;
            StartChat();
        });
        turkyButton.onClick.AddListener(() =>
        {
            MenuSettings.Instance.Languages = Languages.Turkish;
            StartChat();
        });

    }

    public override void OnEnable()
    {
        base.OnEnable();
        chatButtonStatus = false;
        flagsButtonAnimation.Play("Default");
    }



    private void OnStartChatClicked()
    {

        if (chatButtonStatus == true)
        {
            flagsButtonAnimation.Play("off");
        }
        else
        {
            flagsButtonAnimation.Play("on");
        }

        chatButtonStatus = !chatButtonStatus;


    }

    private void StartChat()
    {
        ChatMenu chatMenu = (ChatMenu)ChatMenu.Show();
        chatMenu.Bind(this.meal);
    }

    public void Bind(Meal male)
    {
        this.meal = male;
        mealName.text = male.Name;
        mealDiscription.text = male.Description + " of " + " vale of play some data and should replace later" +
            "dummy data here of play some dummy data and should replace later and good day";
        mealIcon.sprite = male.bigImg;

        mealComponent.text = "";

        for (int i = 0; i < male.components.Count; i++)
        {
            mealComponent.text += "-" + male.components[i] + "\n";
        }


        mealHeathyData.text = meal.GetHealthData();


        // menu3DView.LoadChracter3dFromResources(male.Name);
        //todo 3d mode using male.Name;
        //Start3DView();

    }

    

    void Start3DView()
    {
        var threeDMenu = (ViewThreeDMenu)ViewThreeDMenu.Show();
        threeDMenu.Bind(meal.Name);

    }

    void StartArView()
    {

        var arMenu = (ViewARMenu)ViewARMenu.Show();
        arMenu.Bind(meal.Name);

    }




}
