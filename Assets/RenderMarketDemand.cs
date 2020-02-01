using Assets.Core;

public class RenderMarketDemand : ParameterView
{
    public override string RenderValue()
    {
        var demand = Marketing.GetClientFlow(Q, SelectedNiche);
        var audience = Markets.GetAudienceSize(Q, SelectedNiche);

        return $"{Format.Minify(audience)} users"; // (+{Format.Minify(demand)} weekly)";
    }
}
