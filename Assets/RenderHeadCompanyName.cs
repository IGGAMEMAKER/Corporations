using Assets.Core;

public class RenderHeadCompanyName : ParameterView
{
    public override string RenderValue()
    {
        var cost = Economy.GetCompanyCost(Q, MyCompany);

        return Visuals.Link($"Head company: {MyCompany.company.Name} ({Format.Money(cost)})");
    }
}
