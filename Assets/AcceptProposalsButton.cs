using Assets.Core;

public class AcceptProposalsButton : ButtonController
{
    public override void Execute()
    {
        // + accept investments
        Companies.AcceptAllInvestmentProposals(MyCompany, Q);

        MyCompany.RemoveAcceptsInvestments();

        var goal = FindObjectOfType<InestmentProposalScreen>().Goal;

        Investments.AddCompanyGoal(Flagship, goal);
    }
}
