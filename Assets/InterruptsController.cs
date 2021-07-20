using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InterruptsController : View
{
    public EventView PromoteTeam;
    public EventView Bankruptcy;
    public EventView UnhappyManager;
    public EventView UpgradeFeature;

    public override void ViewRender()
    {
        base.ViewRender();

        var team = Flagship.team.Teams[0];

        var unhappyCoreWorkers = team.Managers.Select(humanId => Humans.Get(Q, humanId))
            .Where(human => human.humanCompanyRelationship.Morale < 30 && Teams.GetLoyaltyChangeForManager(human, team, Flagship) < 0);



        Draw(Bankruptcy, Economy.IsWillBecomeBankruptOnNextPeriod(Q, MyCompany));
        Draw(UnhappyManager, unhappyCoreWorkers.Any());
        Draw(PromoteTeam, false);

        var iterationProgress = Products.GetIterationProgress(Flagship) + CurrentIntDate % (int)C.PERIOD;
//        Debug.Log("progress: " + iterationProgress);
        UpgradeFeature.SetProgress(iterationProgress);
    }
}
