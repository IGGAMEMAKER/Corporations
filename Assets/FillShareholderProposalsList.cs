using Assets.Utils;

public class FillShareholderProposalsList : View
{
    void OnEnable()
    {
        Render();
    }

    void Render()
    {
        var proposals = CompanyUtils.GetInvestmentProposals(GameContext, SelectedCompany.company.Id);

        GetComponent<ShareholderProposalsListView>()
            .SetItems(proposals.ToArray(), SelectedCompany);
    }
}
