using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
