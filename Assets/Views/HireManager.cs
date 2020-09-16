using Assets.Core;
using System.Collections;
using UnityEngine;

public class HireManager : ButtonController
{
    public JobOfferScreen jobOfferScreen;

    public override void Execute()
    {
        var human = SelectedHuman;
        bool hasWorkAlready = human.hasWorker && human.worker.companyId != -1;

        var company = Flagship;

        if (hasWorkAlready)
            Teams.HuntManager(human, company, Q, SelectedTeam);
        else
            Teams.HireManager(company, human, SelectedTeam);

        Teams.SetJobOffer(company, SelectedTeam, human.human.Id, jobOfferScreen.JobOffer);

        ScreenUtils.SetMainPanelId(Q, 1);
        NavigateToMainScreen();

        //GoBack();
        //GoBack();
    }
}
