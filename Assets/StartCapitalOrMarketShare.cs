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
            var maintenance = Markets.GetBiggestMaintenanceOnMarket(GameContext, SelectedNiche);

            return Visuals.Colorize(Format.Money(maintenance), Economy.IsCanMaintain(MyCompany, GameContext, maintenance));
        }
    }
}
