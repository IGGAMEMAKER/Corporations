using Assets.Core;

public class CreateBigCrossfunctionalTeam : CompanyUpgradeButton
{
    public override void Execute()
    {
        Teams.AddTeam(Flagship, TeamType.BigCrossfunctionalTeam);
    }

    public override string GetBenefits()
    {
        return "+3 channels and +2 features at time";
    }

    public override string GetButtonTitle()
    {
        return "Create big crossfunctional team";
    }

    public override string GetHint()
    {
        return "";
    }

    public override bool GetState()
    {
        return true;
    }
}
