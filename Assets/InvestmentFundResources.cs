using Assets.Utils;

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
