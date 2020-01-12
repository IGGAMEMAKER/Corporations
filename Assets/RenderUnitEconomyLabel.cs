using Assets.Core;

public class RenderUnitEconomyLabel : ParameterView
{
    public override string RenderValue()
    {
        var product = SelectedCompany;

        var ads = Markets.GetClientAcquisitionCost(product.product.Niche, GameContext) * 1000;
        var income = Economy.GetBaseSegmentIncome(GameContext, product, 0) * 1000;

        var change = income - ads;

        if (change >= 0)
            return "Unit economy is " + Visuals.Positive("OK");
        else
            return "Unit economy is " + Visuals.Negative("Bad");
    }
}