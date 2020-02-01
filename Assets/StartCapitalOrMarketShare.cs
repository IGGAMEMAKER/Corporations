using Assets.Core;
using System.Linq;

public class StartCapitalOrMarketShare : ParameterView
{
    public override string RenderValue()
    {
        var hasCompany = Companies.HasCompanyOnMarket(MyCompany, SelectedNiche, Q);

        if (hasCompany)
        {
            var control = Companies.GetControlInMarket(MyCompany, SelectedNiche, Q);
            var previousControl = Companies.GetDaughterCompaniesOnMarket(MyCompany, SelectedNiche, Q)
                .Select(p => Companies.GetProductCompanyResults(p, Q))
                .Select(r => r.MarketShareChange)
                .Sum();

            var diff = (long)(previousControl);

            return $"{control}% ({Visuals.PositiveOrNegativeMinified(diff)}%)";
        }
        else
        {
            var maintenance = Markets.GetBiggestMaintenanceOnMarket(Q, SelectedNiche);

            return Visuals.Colorize(Format.Money(maintenance), Economy.IsCanMaintain(MyCompany, Q, maintenance));
        }
    }
}
