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
    public EventView UnhappyTeam;
    public EventView UpgradeFeature;

    public override void ViewRender()
    {
        base.ViewRender();

        var team = Flagship.team.Teams[0];

        var unhappyCoreWorkers = team.Managers.Select(humanId => Humans.Get(Q, humanId))
            .Any(human => human.humanCompanyRelationship.Morale < 30 && Teams.GetLoyaltyChangeForManager(human, team, Flagship) < 0);

        var unhappyTeams = Flagship.team.Teams.Any(t => t.isManagedBadly);

        Draw(Bankruptcy, Economy.IsWillBecomeBankruptOnNextPeriod(Q, MyCompany));
        Draw(UnhappyManager, unhappyCoreWorkers);
        Draw(UnhappyTeam, unhappyTeams);
        Draw(PromoteTeam, false);

        var month = (int)C.PERIOD * 4;
        var ticking = CurrentIntDate % month;

        var gain = Products.GetIterationMonthlyGain(Flagship); // 4
        // TODO MULTUPLY BY 10? WHY? GOTO ITERATION.CS GetIterationProgress
        var currentPoints = Products.GetIterationProgress(Flagship); // 0...100

        if (gain + currentPoints / 10 > C.ITERATION_PROGRESS)
            gain = C.ITERATION_PROGRESS - currentPoints / 10;

        var gainProgress = gain * 100 * ticking;


        var iterationProgress = currentPoints + gainProgress / month / C.ITERATION_PROGRESS;
        //Debug.LogFormat("gain {0}, gainProg {1}, ticking {2}", gain, gainProgress, ticking);

        /*if (currentPoints > C.ITERATION_PROGRESS)
            iterationProgress = 100;*/

        UpgradeFeature.SetProgress(iterationProgress);
    }
}
