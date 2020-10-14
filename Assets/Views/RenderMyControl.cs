using Assets;
using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class RenderMyControl : ParameterView
{
    int previousControl = -1;

    public override string RenderValue()
    {
        var shareholderId = Hero.shareholder.Id;
        var control = Companies.GetShareSize(Q, MyCompany, shareholderId);

        // control changed
        if (previousControl != control)
        {
            if (previousControl != -1)
            {
                Animate(Visuals.Positive("Raised Investments"), null);
                //SoundManager.PlayFastCashSound();
                //gameObject.AddComponent<TextBlink>();
            }
        }

        previousControl = control;


        Colorize(control, 0, 100);

        return Mathf.Floor(control) + "%";
    }
}
