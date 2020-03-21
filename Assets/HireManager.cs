using Assets.Core;

public class HireManager : ButtonController
{
    public override void Execute()
    {
        var human = SelectedHuman;
        bool hasWorkAlready = human.hasWorker && human.worker.companyId != -1;

        var company = Companies.GetFlagship(Q, MyCompany);

        if (hasWorkAlready)
            Teams.HuntManager(human, company, Q);
        else
            Teams.HireManager(company, human);

        GoBack();
        GoBack();

        //NavigateToMainScreen();

        //NavigateToProjectScreen(company.company.Id);
    }
}
