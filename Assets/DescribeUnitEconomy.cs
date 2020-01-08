using Assets.Core;

public class DescribeUnitEconomy : ParameterView
{
    public override string RenderValue()
    {
        var isProfitable = Economy.IsProfitable(GameContext, SelectedCompany);

        // Your business will be " + Visuals.Positive("profitable") + ". 
        if (isProfitable)
            return "Unit economy is " + Visuals.Positive("OK") + "\nGet as more clients as you can!";
        else
            return "Unit economy is " + Visuals.Negative("Bad") + "\nAdd more features to succeed!";
    }
}
