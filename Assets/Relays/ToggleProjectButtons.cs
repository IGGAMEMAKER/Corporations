using Assets.Core;
using UnityEngine;

public class ToggleProjectButtons : View
{
    public GameObject Economy;
    public GameObject Investors;
    public GameObject Development;
    public GameObject Team;

    public GameObject Research;


    public override void ViewRender()
    {
        base.ViewRender();

        var company = SelectedCompany;

        var isRelatedToPlayer = Companies.IsRelatedToPlayer(GameContext, company);
        var isExplored = company.hasResearch || isRelatedToPlayer;


        var hasExplorationTask = Cooldowns.IsHasTask(GameContext, new CompanyTaskExploreCompany(company.company.Id));
        var isResearchingOrDone = isExplored || hasExplorationTask;
        Research.SetActive(!isResearchingOrDone);

        Economy.SetActive(false && isExplored);
        Investors.SetActive(isExplored);

        // product only
        Development.SetActive(isExplored && company.hasProduct);
        Team.SetActive(isExplored);
    }
}
