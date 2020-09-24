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
        bool worksInPlayerFlagship = human.worker.companyId == Flagship.company.Id;

        int teamId = worksInPlayerFlagship ? Teams.GetTeamOf(human, Q).ID : -1;

        var company = Flagship;

        if (hasWorkAlready)
        {
            if (worksInPlayerFlagship)
            {
                // salary upgrade
                Teams.SetJobOffer(company, teamId, human.human.Id, jobOfferScreen.JobOffer);
            }
            else
            {
                Teams.HuntManager(human, company, Q, SelectedTeam);
            }
        }
        else
            Teams.HireManager(company, human, SelectedTeam);

        Teams.SetJobOffer(company, SelectedTeam, human.human.Id, jobOfferScreen.JobOffer);

        ScreenUtils.SetMainPanelId(Q, 1);
        NavigateToMainScreen();

        //GoBack();
        //GoBack();
    }
}
