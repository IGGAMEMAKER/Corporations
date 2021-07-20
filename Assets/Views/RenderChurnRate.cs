using Assets.Core;

public class RenderChurnRate : UpgradedParameterView
{
    public override string RenderHint()
    {
        var churnRate = Marketing.GetChurnRate(company, Q, true);

        return churnRate.ToString(true);
    }

    GameEntity company => Flagship;

    public override string RenderValue()
    {
        var churn = Marketing.GetChurnClients(company, Q);
        var churnRate = Marketing.GetChurnRate(company, Q);

        return Visuals.Negative(Format.Minify(-churn) + " weekly"); // (" + (int)churnRate + "%)
    }
}
