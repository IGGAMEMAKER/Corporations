using Assets.Core;

public class HireManager : ButtonController
{
    public JobOfferScreen jobOfferScreen;

    public override void Execute()
    {
        var human = SelectedHuman;
        var company = Flagship;

        bool hasWorkAlready = Humans.IsEmployed(human);
        bool worksInPlayerFlagship = human.worker.companyId == Flagship.company.Id;

        int teamId = worksInPlayerFlagship ? Teams.GetTeamOf(human, Q).ID : -1;

        if (hasWorkAlready)
        {
            if (worksInPlayerFlagship)
            {
                // salary upgrade
                
                Teams.SetJobOffer(human, company, jobOfferScreen.JobOffer, teamId, Q);
            }
            else
            {
                Teams.HuntManager(human, company, Q, SelectedTeam);
            }
        }
        else
        {
            Teams.HireManager(company, Q, human, SelectedTeam);
        }

        Teams.SetJobOffer(human, company, jobOfferScreen.JobOffer, SelectedTeam, Q);

        ScreenUtils.SetMainPanelId(Q, 1);
        Navigate(ScreenMode.TeamScreen);

        //NavigateToMainScreen();

        //GoBack();
        //GoBack();
    }
}
