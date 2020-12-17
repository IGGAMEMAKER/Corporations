using Assets.Core;

public class RenderEconomyTabName : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        var cost = Economy.CostOf(SelectedCompany, Q);

        return $"ECONOMY ({Format.Money(cost)})";
    }
}
