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

        var isExplored = SelectedCompany.hasResearch || Companies.IsRelatedToPlayer(GameContext, SelectedCompany);


        var hasExplorationTask = Cooldowns.IsHasTask(GameContext, new CompanyTaskExploreCompany(SelectedCompany.company.Id));
        var isResearchingOrDone = isExplored || hasExplorationTask;
        Research.SetActive(!isResearchingOrDone);

        Economy.SetActive(isExplored);
        Investors.SetActive(isExplored);
        Development.SetActive(isExplored);
        Team.SetActive(isExplored);
    }
}
