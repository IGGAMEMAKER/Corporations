using Assets.Core;

public class DescribeUnitEconomy : ParameterView
{
    public override string RenderValue()
    {
        var product = SelectedCompany;

        var ads = Markets.GetClientAcquisitionCost(product.product.Niche, GameContext) * 1000;
        var income = Economy.GetSegmentPrice(GameContext, product, 0) * 1000;

        var change = income - ads;

        //var isProfitable = Economy.IsProfitable(GameContext, SelectedCompany);
        var isProfitable = change >= 0;

        // Your business will be " + Visuals.Positive("profitable") + ". 
        //if (isProfitable)
        //    return "Unit economy is " + Visuals.Positive("OK") + "\nGet as more clients as you can!";
        //else
        //    return "Unit economy is " + Visuals.Negative("Bad") + "\nAdd more features to succeed!";
        if (isProfitable)
            return Visuals.Positive("Get as more clients as you can!");
        else
            return Visuals.Negative("Add more features to become profitable!");
    }
}
