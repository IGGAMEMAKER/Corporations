using UnityEngine;

public class PossibleInvestorsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data)
    {
        var investor = entity as GameEntity;

        t.GetComponent<PossibleInvestor>()
            .SetEntity(investor);
    }
}
