using Assets.Core;

public class HireManager : ButtonController
{
    public override void Execute()
    {
        Teams.HireManager(SelectedCompany, SelectedHuman);
    }
}
