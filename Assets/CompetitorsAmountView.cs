using Assets.Utils;
using System;

public class CompetitorsAmountView : ParameterView
{
    public override string RenderHint()
    {
        return "All competitors\n\n" + String.Join("\n", NicheUtils.GetCompetitorInfo(myProductEntity, GameContext));
    }

    public override string RenderValue()
    {
        return NicheUtils.GetCompetitorsAmount(myProductEntity, GameContext) + "";
    }
}
