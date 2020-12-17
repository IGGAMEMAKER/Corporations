using Assets.Core;

public class HideWorkerButtons : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return false;
        var flagshipId = Companies.GetPlayerFlagshipID(Q);

        bool isFlagship = SelectedCompany.company.Id == flagshipId;

        return !isFlagship;
    }
}
