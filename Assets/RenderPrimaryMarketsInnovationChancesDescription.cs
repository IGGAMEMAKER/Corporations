using Assets.Utils;
using Assets.Utils.Formatting;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RenderPrimaryMarketsInnovationChancesDescription : View
{
    public Text MarketLimitDescription;

    public override void ViewRender()
    {
        base.ViewRender();

        var innovationBonus = ProductUtils.GetFocusingBonus(MyCompany);
        var markets = string.Join("\n* ", MyCompany.companyFocus.Niches.Select(EnumUtils.GetFormattedNicheName));

        MarketLimitDescription.text = $"You have +{Visuals.Positive(innovationBonus.ToString())}% chance to innovate on these markets\n" +
            markets
            ;
    }
}