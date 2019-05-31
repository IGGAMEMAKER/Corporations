using UnityEngine;

public class ShareholderProposalsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data)
    {
        var proposal = entity as GameEntity;

        t.GetComponent<ShareholderProposalView>()
            .SetEntity(proposal);
    }
}
