using Assets.Core;

public class RenderChurnRate : UpgradedParameterView
{
    public override string RenderHint()
    {
        var churnRate = Marketing.GetChurnRate(company, 0, Q, true);

        return churnRate.ToString(true);
    }

    GameEntity company => Flagship;

    public override string RenderValue()
    {
        var churn = Marketing.GetChurnClients(company, Q, 0);

        return Visuals.Negative(Format.Minify(-churn));
    }
}
