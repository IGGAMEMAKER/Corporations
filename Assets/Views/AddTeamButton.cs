using Assets.Core;

public class AddTeamButton : ButtonController
{
    public TeamType TeamType;
    public override void Execute()
    {
        Teams.AddTeam(Flagship, TeamType);

        //FindObjectOfType<FlagshipRelayInCompanyView>().ChooseWorkerInteractions();
    }

    //public override string GetBenefits()
    //{
    //    return "";
    //}

    //public override string GetButtonTitle()
    //{
    //    switch (TeamType)
    //    {
    //        default: return "Create new " + TeamType.ToString() + " team";
    //    }
    //}

    //public override string GetHint()
    //{
    //    return "";
    //}

    //public override bool GetState()
    //{
    //    return true;
    //}
}
