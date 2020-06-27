using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlagshipInterruptsView : View
{
    public Image NeedsServersImage;
    public Image NeedsSupportImage;
    public Image NeedsManagersImage;
    public Image DDOSImage;

    //
    public Image HasDisloyalManagersImage;

    public override void ViewRender()
    {
        base.ViewRender();


        var product = Flagship;

        bool needsMoreServers = Products.IsNeedsMoreServers(product);
        bool needsMoreSupport = Products.IsNeedsMoreMarketingSupport(product);
        bool needsMoreManagers = product.team.Managers.Count < Teams.GetRolesTheoreticallyPossibleForThisCompanyType(product).Count;
        bool underAttack = false;

        // 
        bool workerDisloyal = false;

        Draw(NeedsManagersImage, needsMoreManagers);
        Draw(NeedsServersImage, needsMoreServers);
        Draw(NeedsSupportImage, needsMoreSupport);
        Draw(DDOSImage, underAttack);

        Draw(HasDisloyalManagersImage, workerDisloyal);
    }
}
