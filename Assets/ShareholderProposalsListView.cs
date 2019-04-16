using UnityEngine;

public class ShareholderProposalsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data)
    {
        InvestmentProposal proposal = entity as InvestmentProposal;
        // data is company

        t.GetComponent<ShareholderProposalView>()
            .SetEntity(proposal);
    }
}
