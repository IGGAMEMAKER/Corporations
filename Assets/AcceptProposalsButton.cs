using Assets.Core;

public class AcceptProposalsButton : ButtonController
{
    public override void Execute()
    {
        var proposalScreen = FindObjectOfType<InestmentProposalScreen>();

        // sets urgency
        proposalScreen.UpdateStartDates();

        Companies.AcceptAllInvestmentProposals(MyCompany, Q);
        MyCompany.RemoveAcceptsInvestments();

        // set goal
        Investments.AddCompanyGoal(MyCompany, Q, proposalScreen.Goal);
    }
}
