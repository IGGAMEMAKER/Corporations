﻿using Assets.Core;
using UnityEngine;

public class RenderMyControl : ParameterView
{
    int previousControl = -1;

    public override string RenderValue()
    {
        var shareholderId = Hero.shareholder.Id;
        var control = Companies.GetShareSize(Q, MyCompany, Hero);

        // control changed
        if (previousControl != control)
        {
            if (previousControl != -1)
            {
                //Animate(Visuals.Positive("Raised Investments"), null);

                //SoundManager.PlayFastCashSound();
                //gameObject.AddComponent<TextBlink>();
            }
        }

        previousControl = control;


        Colorize(control, 0, 100);

        return Mathf.Floor(control) + "%";
    }
}
