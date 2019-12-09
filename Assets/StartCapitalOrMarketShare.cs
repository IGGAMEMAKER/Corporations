using Assets.Utils;

public class StartCapitalOrMarketShare : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var hasCompany = Companies.HasCompanyOnMarket(MyCompany, SelectedNiche, GameContext);

        if (hasCompany)
        {
            return Companies.GetControlInMarket(MyCompany, SelectedNiche, GameContext) + "%";
        }
        else
        {
            var capital = NicheUtils.GetStartCapital(SelectedNiche, GameContext);

            return Visuals.Colorize(Format.Money(capital), Companies.IsEnoughResources(MyCompany, capital));
        }
    }
}
