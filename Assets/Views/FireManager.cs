using Assets.Core;

public class FireManager : ButtonController
{
    public override void Execute()
    {
        var companyId = SelectedHuman.worker.companyId;

        if (companyId < 0)
            return;

        Teams.FireManager(Q, SelectedHuman);

        GoBack();
        //GoBack();
        //NavigateToMainScreen();

        //NavigateToProjectScreen(companyId);
    }
}
