using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleDevelopmentController : ButtonController
{
    public Text Text;

    public void OnEnable()
    {
        Render();
    }

    public override void Execute()
    {
        ProductDevelopmentUtils.ToggleDevelopment(GameContext, MyProductEntity.company.Id);

        Render();
    }

    void Render()
    {
        Text.text = MyProductEntity.isDevelopmentActive ? "Pause platform development" : "Enable platform development";

        AddIsChosenComponent(MyProductEntity.isDevelopmentActive);
    }
}
