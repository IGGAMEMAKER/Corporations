using Assets.Core;
using System.Linq;

public class HideCaptureMarketButtonIfHaveNoCompaniesOnThisMarket : HideOnSomeCondition
{
    public override bool HideIf()
    {
        var hasReleasedCompaniesOnMarket = Companies.GetDaughtersOnMarket(MyCompany, SelectedNiche, Q).Count(c => c.isRelease) > 0;

        return !hasReleasedCompaniesOnMarket;
    }
}
