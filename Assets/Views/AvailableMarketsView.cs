using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableMarketsView : ParameterView
{
    public override string RenderValue()
    {
        var company = Flagship;

        var activeMarkets = Marketing.GetAmountOfEnabledChannels(company);
        var maxChannels = Marketing.GetAmountOfChannelsThatYourTeamCanReach(company);

        bool maxedOut = activeMarkets >= maxChannels;

        var helpPhrase = maxedOut ? "(hire more teams)" : "";

        return $"{activeMarkets} / {maxChannels} markets active {helpPhrase}";
    }
}
