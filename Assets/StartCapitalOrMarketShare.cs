using Assets.Utils;
using System.Linq;

public class StartCapitalOrMarketShare : ParameterView
{
    public override string RenderValue()
    {
        var hasCompany = Companies.HasCompanyOnMarket(MyCompany, SelectedNiche, GameContext);

        if (hasCompany)
        {
            var control = Companies.GetControlInMarket(MyCompany, SelectedNiche, GameContext);
            var previousControl = Companies.GetDaughterCompaniesOnMarket(MyCompany, SelectedNiche, GameContext)
                .Select(p => Companies.GetProductCompanyResults(p, GameContext))
                .Select(r => r.MarketShareChange)
                .Sum();

            var diff = (long)(previousControl);

            return $"{control}% ({Visuals.PositiveOrNegativeMinified(diff)}%)";
        }
        else
        {
            var maintenance = Markets.GetBiggestMaintenanceOnMarket(GameContext, SelectedNiche);

            return Visuals.Colorize(Format.Money(maintenance), Economy.IsCanMaintain(MyCompany, GameContext, maintenance));
        }
    }
}
