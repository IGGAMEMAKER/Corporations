using Assets.Core;

public class RenderChurnRate : UpgradedParameterView
{
    public override string RenderHint()
    {
        var bonus = new Bonus<long>("Audience change");

        var churnBonus = Marketing.GetChurnBonus(Q, company.company.Id);
        churnBonus.SetDimension("%");
        var churnUsers = Marketing.GetChurnClients(Q, company.company.Id);

        bonus.Append("Growth", Marketing.GetAudienceGrowth(company, Q));
        bonus.Append("Loss", -churnUsers);
        bonus.MinifyValues();

        return Visuals.Positive(bonus.ToString()) + "\n" + churnBonus.ToString(true);
    }

    GameEntity company => Flagship;

    public override string RenderValue()
    {
        var rate = Marketing.GetChurnRate(Q, company.company.Id).ToString();
        var churnUsers = Marketing.GetChurnClients(Q, company.company.Id);

        var growth = Marketing.GetAudienceGrowth(company, Q);

        var change = growth - churnUsers;

        Colorize(Visuals.GetColorPositiveOrNegative(change));

        var sign = "";

        if (change > 0)
            sign = "+";



        return sign + Format.Minify(change);

        //return $"{Format.Minify(churnUsers)} users ({rate}%)";
        //return MarketingUtils.GetChurnBonus(GameContext, SelectedCompany.company.Id).Sum();
    }
}
