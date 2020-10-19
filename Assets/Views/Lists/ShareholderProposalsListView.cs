using Assets.Core;
using UnityEngine;

public class ShareholderProposalsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        var proposal = entity as InvestmentProposal;

        t.GetComponent<ShareholderProposalView>().SetEntity(proposal);
    }

    public override void ViewRender()
    {
        var proposals = Companies.GetInvestmentProposals(MyCompany);

        GetComponent<ShareholderProposalsListView>().SetItems(proposals);
    }
}
