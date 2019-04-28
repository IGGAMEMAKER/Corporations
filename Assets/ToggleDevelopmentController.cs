using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleDevelopmentController : ButtonController
    , IDevelopmentActiveListener
{
    public Text Text;

    //public override void ButtonStart()
    //{
    //    base.ButtonStart();

    //    MyProductEntity.AddDevelopmentActiveListener(this);
    //}

    public override void Execute()
    {
        ProductDevelopmentUtils.ToggleDevelopment(GameContext, MyProductEntity.company.Id);
    }

    void IDevelopmentActiveListener.OnDevelopmentActive(GameEntity entity)
    {
        Render();
    }

    void Render()
    {
        Text.text = MyProductEntity.isDevelopmentActive ? "Pause platform development" : "Enable platform development";

        AddIsChosenComponent(MyProductEntity.isDevelopmentActive);
    }

    void Update()
    {
        Render();
    }
}
