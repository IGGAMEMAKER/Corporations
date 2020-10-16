using Assets.Core;

public class SetVotingStyleController : ButtonController
{
    public VotingStyle VotingStyle;

    public override void Execute()
    {
        Investments.SetVotingStyle(MyCompany, VotingStyle);
    }
}
