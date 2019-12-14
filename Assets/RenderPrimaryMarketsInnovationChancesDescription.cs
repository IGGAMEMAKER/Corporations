using Assets.Utils;
using Assets.Utils.Formatting;
using System.Linq;
using UnityEngine.UI;

public class RenderPrimaryMarketsInnovationChancesDescription : View
{
    public Text MarketLimitDescription;

    public override void ViewRender()
    {
        base.ViewRender();

        var innovationBonus = Products.GetFocusingBonus(MyCompany);
        var markets = string.Join("\n", MyCompany.companyFocus.Niches.Select(n => "*" + EnumUtils.GetFormattedNicheName(n)));
        markets = "";

        MarketLimitDescription.text = $"You have +{Visuals.Positive(innovationBonus.ToString())}% chance to innovate on primary markets" +
            markets
            ;
    }
}