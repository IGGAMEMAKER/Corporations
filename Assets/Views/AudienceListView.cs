using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudienceListView : ListView
{
    int Loyalties;

    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<AudiencePreview>().SetEntity((AudienceInfo)(object)entity, Flagship, Loyalties);
    }

    public void SetAudiences(List<AudienceInfo> audiences, int loyalties)
    {
        Loyalties = loyalties;
        SetItems(audiences);
    }
}
