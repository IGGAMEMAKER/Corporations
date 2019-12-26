using Assets.Core;

public class InvestmentFundResources : SimpleParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        return Format.Minify(MyGroupEntity.companyResource.Resources.money);
    }
}
