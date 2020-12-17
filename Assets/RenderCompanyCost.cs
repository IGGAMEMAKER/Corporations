using Assets.Core;

public class RenderCompanyCost : ParameterView
{
    public override string RenderValue()
    {
        return Format.MinifyMoney(Economy.CostOf(MyCompany, Q));
    }
}
