using Assets.Core;

public class RenderCompanyRank : UpgradedParameterView
{
    public override string RenderHint() => "";

    public override string RenderValue()
    {
        var rank = Economy.CostOf(MyCompany, Q);

        return $"{Format.Money(rank)}";
    }
}
