using Assets.Utils;

public class StartCapitalOrMarketShare : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        var hasCompany = CompanyUtils.HasCompanyOnMarket(MyCompany, SelectedNiche, GameContext);

        if (hasCompany)
        {
            return CompanyUtils.GetControlInMarket(MyCompany, SelectedNiche, GameContext) + "%";
        }
        else
        {
            var capital = NicheUtils.GetStartCapital(SelectedNiche, GameContext);

            return Visuals.Colorize(Format.Money(capital), CompanyUtils.IsEnoughResources(MyCompany, capital));
        }
    }
}
