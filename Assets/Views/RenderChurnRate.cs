using Assets.Core;

public class RenderChurnRate : UpgradedParameterView
{
    public override string RenderHint()
    {
        return Marketing.GetChurnBonus(Q, SelectedCompany.company.Id).ToString(true);
    }

    public override string RenderValue()
    {
        var rate = Marketing.GetChurnRate(Q, SelectedCompany.company.Id).ToString();
        var churnUsers = Marketing.GetChurnClients(Q, SelectedCompany.company.Id);

        return $"Loses {Format.Minify(churnUsers)} users weekly ({rate}% churn)";

        return $"{Format.Minify(churnUsers)} users ({rate}%)";
        //return MarketingUtils.GetChurnBonus(GameContext, SelectedCompany.company.Id).Sum();
    }
}
