using Assets.Core;

public class RenderChurnRate : UpgradedParameterView
{
    public override string RenderHint()
    {
        var bonus = Marketing.GetAudienceChange(company, Q, true);

        //var churnBonus = Marketing.GetChurnBonus(gameContext, product.company.Id);
        //churnBonus.SetDimension("%");

        return "Churn rate is: " + Marketing.GetChurnRate(company, 0) + " due to\n\n" + Marketing.GetSegmentLoyalty(company, 0, true).ToString();
        return Marketing.GetChurnRate(company, 0, true).ToString(true);
        return Visuals.Positive(bonus.ToString()); // + "\n" + churnBonus.ToString(true);
    }

    GameEntity company => Flagship;

    public override string RenderValue()
    {
        return Visuals.Negative(Format.Minify(-Marketing.GetChurnClients(company, 0)));
    }
}
