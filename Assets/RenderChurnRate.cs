using Assets.Core;

public class RenderChurnRate : UpgradedParameterView
{
    public override string RenderHint()
    {
        return Marketing.GetChurnBonus(GameContext, SelectedCompany.company.Id).ToString(true);
    }

    public override string RenderValue()
    {
        var rate = Marketing.GetChurnRate(GameContext, SelectedCompany.company.Id).ToString();
        var churnUsers = Marketing.GetChurnClients(GameContext, SelectedCompany.company.Id);

        return $"Loses {Format.Minify(churnUsers)} users weekly ({rate}% churn)";

        return $"{Format.Minify(churnUsers)} users ({rate}%)";
        //return MarketingUtils.GetChurnBonus(GameContext, SelectedCompany.company.Id).Sum();
    }
}
