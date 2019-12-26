using Assets.Core;

public class RenderDesireToSellCompany : UpgradedParameterView
{
    public override string RenderHint()
    {
        var text = "You need at least 75% to buy company guaranteedly. Otherwise, buy shares one by one";

        var condition = Companies.IsWillSellCompany(SelectedCompany, GameContext) ?
            Visuals.Positive(text) : Visuals.Negative(text);

        return $"{Visuals.Positive(Desire + "%")} of shareholders want to sell their shares\n" +
            $"{Visuals.Negative((100 - Desire) + "%")} will refuse to sell shares\n\n {condition}";
    }

    long Desire
    {
        get
        {
            return Companies.GetDesireToSellCompany(SelectedCompany, GameContext);
        }
    }

    public override string RenderValue()
    {
        return "Desire to sell company: " + Desire + "%";
    }
}
