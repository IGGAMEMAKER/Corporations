using Assets.Core;

public class DescribeUnitEconomy : ParameterView
{
    public override string RenderValue()
    {
        var product = SelectedCompany;

        var ads = Markets.GetClientAcquisitionCost(product.product.Niche, GameContext) * 1000;
        var income = Economy.GetBaseSegmentIncome(GameContext, product, 0) * 1000;

        var change = income - ads;

        if (change >= 0)
            return "Get as more clients as you can!";
        else
            return "Add more features to become profitable!";
    }
}
