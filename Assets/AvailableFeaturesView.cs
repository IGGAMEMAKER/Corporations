using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableFeaturesView : ParameterView
{
    public override string RenderValue()
    {
        var company = Flagship;

        var activeMarkets = Products.GetAmountOfUpgradingFeatures(company, Q);
        var maxChannels = Products.GetAmountOfFeaturesThatYourTeamCanUpgrade(company);

        bool maxedOut = activeMarkets >= maxChannels;

        var helpPhrase = maxedOut ? "(hire more teams)" : "";

        return $"{activeMarkets} / {maxChannels} features upgrading {helpPhrase}";
    }
}
