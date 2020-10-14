using Assets.Core;

public class RenderHeadCompanyName : ParameterView
{
    public override string RenderValue()
    {
        var cost = Economy.CostOf(MyCompany, Q);

        return Visuals.Link($"Head company: {MyCompany.company.Name} ({Format.Money(cost)})");
    }
}
