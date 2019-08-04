using Assets.Utils;
using System.Linq;

public class LinkToMainNiche : ButtonController
{
    public override void Execute()
    {
        var focus = MyCompany.companyFocus.Niches;

        if (focus.Count == 0)
            return;

        var mostValuableNiche = focus
            .OrderByDescending(n => CompanyUtils.GetMarketImportanceForCompany(GameContext, MyCompany, n))
            .First();

        NavigateToNiche(mostValuableNiche);
    }
}
