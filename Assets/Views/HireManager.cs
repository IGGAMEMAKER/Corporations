using Assets.Core;
using System.Collections;
using UnityEngine;

public class HireManager : ButtonController
{
    public override void Execute()
    {
        var human = SelectedHuman;
        bool hasWorkAlready = human.hasWorker && human.worker.companyId != -1;

        var company = Flagship;

        if (hasWorkAlready)
            Teams.HuntManager(human, company, Q, SelectedTeam);
        else
            Teams.HireManager(company, human, SelectedTeam);

        ScreenUtils.SetMainPanelId(Q, 1);
        NavigateToMainScreen();
        //ReturnToHiringManagers();

        //GoBack();
        //GoBack();
    }

    IEnumerator ReturnToHiringManagers()
    {
        Debug.Log("Hire manager");
        yield return new WaitForSeconds(0.45f);

        Debug.Log("Hire manager after delay");
        FindObjectOfType<FlagshipRelayInCompanyView>().ChooseManagersTabs(SelectedTeam);
    }
}
