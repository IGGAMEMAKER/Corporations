using Assets.Core;

public class DescribeUnitEconomy : ParameterView
{
    public override string RenderValue()
    {
        var product = SelectedCompany;

        var ads = Markets.GetClientAcquisitionCost(product.product.Niche, Q) * 1000;
        var income = Economy.GetBaseSegmentIncome(Q, product) * 1000;

        var change = income - ads;

        if (change >= 0)
            return "Get as more clients as you can!";
        else
            return "Add more features to become profitable!";
    }
}
