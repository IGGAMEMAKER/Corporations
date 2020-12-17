using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class RenderMarketShare : ParameterView
{
    public Image DynamicsImage;

    long previousValue = 0;

    public override string RenderValue()
    {
        var control = Companies.GetControlInMarket(MyCompany, Flagship.product.Niche, Q);

        Colorize((int)control, 0, 100);


        var dynamics = control - previousValue;

        previousValue = control;

        DynamicsImage.color = Visuals.GetColorPositiveOrNegative(dynamics);
        DynamicsImage.gameObject.transform.rotation = Quaternion.Euler(0, 0, dynamics > 0 ? 0 : -180f);

        return control.ToString("0.0");
    }
}
