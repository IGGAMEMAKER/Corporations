using Assets.Core;

public class CreateBigCrossfunctionalTeam : CompanyUpgradeButton
{
    public override void Execute()
    {
        Teams.AddTeam(Flagship, TeamType.BigCrossfunctionalTeam);
    }

    public override string GetBenefits()
    {
        return $"+{Teams.GetAmountOfPossibleChannelsByTeamType(TeamType.BigCrossfunctionalTeam)} channel and +{Teams.GetAmountOfPossibleFeaturesByTeamType(TeamType.BigCrossfunctionalTeam)} feature at time";
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
