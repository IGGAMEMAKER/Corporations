using Assets.Core;

public class RenderCompanyCost : ParameterView
{
    public override string RenderValue()
    {
        return Format.Money(Economy.CostOf(MyCompany, Q), true);
    }
}
