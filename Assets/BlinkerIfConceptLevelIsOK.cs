using Assets.Core;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Blinker))]
public class BlinkerIfConceptLevelIsOK : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        var daughtersOnMarket = Companies.GetDaughterCompaniesOnMarket(MyCompany, SelectedNiche, Q);

        var releaseableApps = daughtersOnMarket.Where(p => !p.isRelease && !Products.IsOutOfMarket(p, Q));

        bool hasReleasebleApps = releaseableApps.Count() > 0;

        GetComponent<Blinker>().enabled = hasReleasebleApps;

        if (hasReleasebleApps)
        {
            GetComponent<ReleaseApp>().SetCompanyId(releaseableApps.First().company.Id);
        }
    }
}
