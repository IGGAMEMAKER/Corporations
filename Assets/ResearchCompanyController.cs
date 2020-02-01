using Assets.Core;

public class ResearchCompanyController : ButtonController
{
    public override void Execute()
    {
        Cooldowns.AddTask(Q, new CompanyTaskExploreCompany(SelectedCompany.company.Id), 8);
    }
}
