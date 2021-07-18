using Assets.Core;

public class RenderChurnRate : UpgradedParameterView
{
    public override string RenderHint()
    {
        var churnRate = Marketing.GetChurnRate(company, Q, 0);
        var loyalty = Marketing.GetSegmentLoyalty(company, 0, true);

        return "Churn rate is: " + churnRate + " due to\n\n" + loyalty.ToString();
    }

    GameEntity company => Flagship;

    public override string RenderValue()
    {
        var churn = Marketing.GetChurnClients(company, Q, 0);

        return Visuals.Negative(Format.Minify(-churn));
    }
}
