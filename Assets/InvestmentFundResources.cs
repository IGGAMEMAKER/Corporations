using Assets.Utils;

public class InvestmentFundResources : ParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        return ValueFormatter.Shorten(MyGroupEntity.shareholder.Money);
    }
}
