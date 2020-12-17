using Assets.Core;

public class RenderMarketState : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "Companies get new clients due to their brand powers";
    }

    string GetGrowthDescription (GameEntity p)
    {
        return "XXX???";
    }

    public override string RenderValue()
    {
        var state = Markets.GetMarketState(Q, SelectedNiche);
        var speed = Markets.GetMarketGrowth(state);


        var flow = Marketing.GetClientFlow(Q, SelectedNiche);

        return Visuals.Positive("+" + speed) + $"% / month";
            //+
            //$"\n\nMarket will get {Format.MinifyToInteger(flow)} new users this month";
    }
}
