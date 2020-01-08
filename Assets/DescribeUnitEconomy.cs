using Assets.Core;

public class DescribeUnitEconomy : ParameterView
{
    public override string RenderValue()
    {
        var isProfitable = Economy.IsProfitable(GameContext, SelectedCompany);

        if (isProfitable)
            return "Unit economy is " + Visuals.Positive("OK") + "\nYour business will be " + Visuals.Positive("profitable") + ". Get as more clients as you can!";
        else
            return "Unit economy is " + Visuals.Negative("Bad") + "\nAdd more features to succeed!";
    }
}
