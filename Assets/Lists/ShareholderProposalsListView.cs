using Assets.Utils;
using UnityEngine;

public class ShareholderProposalsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data)
    {
        var proposal = entity as InvestmentProposal;

        t.GetComponent<ShareholderProposalView>()
            .SetEntity(proposal);
    }

    void Draw()
    {
        var proposals = CompanyUtils.GetInvestmentProposals(GameContext, SelectedCompany.company.Id);

        GetComponent<ShareholderProposalsListView>()
            .SetItems(proposals.ToArray(), SelectedCompany);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Draw();
    }
}
