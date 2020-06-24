using Assets.Core;

public class RenderChurnRate : UpgradedParameterView
{
    public override string RenderHint()
    {
        return Marketing.GetChurnBonus(Q, company.company.Id).ToString(true);
    }

    GameEntity company => Flagship;

    public override string RenderValue()
    {
        var rate = Marketing.GetChurnRate(Q, company.company.Id).ToString();
        var churnUsers = Marketing.GetChurnClients(Q, company.company.Id);

        Colorize(Colors.COLOR_NEGATIVE);

        return Format.Minify(churnUsers);

        //return $"{Format.Minify(churnUsers)} users ({rate}%)";
        //return MarketingUtils.GetChurnBonus(GameContext, SelectedCompany.company.Id).Sum();
    }
}
