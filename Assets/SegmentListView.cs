using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SegmentListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var e = (KeyValuePair<UserType, long>)(object)entity;

        if (t.GetComponent<ClientSegmentView>() != null)
            t.GetComponent<ClientSegmentView>().SetEntity(e.Key, MyProductEntity.company.Id);

        if (t.GetComponent<ClientSegmentPreview>() != null)
            t.GetComponent<ClientSegmentPreview>().SetEntity(MyProductEntity.company.Id);
    }

    void Render()
    {
        if (MyProductEntity == null)
            return;

        Debug.Log("Segment List View removed segments!");
    }


    void OnEnable()
    {
        Render();
    }
}
