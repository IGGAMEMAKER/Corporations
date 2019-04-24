using Assets.Utils;
using System;

public class CompetitorsAmountView : SimpleParameterView
{
    public override string RenderHint()
    {
        return "All competitors\n\n" + String.Join("\n", NicheUtils.GetCompetitorInfo(MyProductEntity, GameContext));
    }

    public override string RenderValue()
    {
        return NicheUtils.GetCompetitorsAmount(MyProductEntity, GameContext) + "";
    }
}
