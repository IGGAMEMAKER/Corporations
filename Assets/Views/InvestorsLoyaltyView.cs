using Assets.Core;
using UnityEngine;
using UnityEngine.UI;

public class InvestorsLoyaltyView : ParameterView
{
    public Image DynamicsImage;

    int previousValue = 0;

    public override string RenderValue()
    {
        var loyalty = Random.Range(0, 100);

        Colorize(loyalty, 0, 100);

        var dynamics = loyalty - previousValue;

        previousValue = loyalty;

        DynamicsImage.color = Visuals.GetColorPositiveOrNegative(dynamics);
        DynamicsImage.gameObject.transform.rotation = Quaternion.Euler(0, 0, dynamics > 0 ? 0 : -180f);

        return loyalty.ToString("0.0") + "%";
    }
}
