using Assets.Utils;

public class RenderChurnRate : UpgradedParameterView
{
    public override string RenderHint()
    {
        return MarketingUtils.GetChurnBonus(GameContext, SelectedCompany.company.Id).ToString(true);
    }

    public override string RenderValue()
    {
        var rate = MarketingUtils.GetChurnRate(GameContext, SelectedCompany.company.Id).ToString();
        var churnUsers = MarketingUtils.GetChurnClients(GameContext, SelectedCompany.company.Id);

        return $"{Format.Minify(churnUsers)} users (-{rate}%)";
        //return MarketingUtils.GetChurnBonus(GameContext, SelectedCompany.company.Id).Sum();
    }
}
