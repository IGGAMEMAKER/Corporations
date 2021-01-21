using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public abstract class ParameterDynamicView : UpgradedParameterView
{
    public Image DynamicsImage;

    long previousValue = 0;

    public override string RenderValue()
    {
        var audience = GetParameter(); // Companies.GetControlInMarket(MyCompany, Flagship.product.Niche, Q);

        //Colorize((int)control, 0, 100);

        bool valueChanged = previousValue != audience;

        var dynamics = audience - previousValue;


        previousValue = audience;

        if (DynamicsImage != null)
        {
            Draw(DynamicsImage, valueChanged);
            DynamicsImage.color = Visuals.GetColorPositiveOrNegative(dynamics);
            DynamicsImage.gameObject.transform.rotation = Quaternion.Euler(0, 0, dynamics > 0 ? 0 : -180f);
        }

        return Format.Minify(audience); //.ToString("0.0");
    }

    public abstract long GetParameter();
}