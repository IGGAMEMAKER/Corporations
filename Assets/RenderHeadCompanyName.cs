using Assets.Utils;

public class RenderHeadCompanyName : ParameterView
{
    public override string RenderValue()
    {
        var cost = Economy.GetCompanyCost(GameContext, MyCompany);

        return Visuals.Link($"Head company: {MyCompany.company.Name} ({Format.MinifyMoney(cost)})");
    }
}
