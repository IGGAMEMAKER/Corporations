using UnityEngine;

public class ManageableOwningsListView : OwningsListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        //base.SetItem(t, entity, data);

        var e = entity as GameEntity;

        var obj = t.gameObject;

        obj.AddComponent<SelectCompanyController>().companyId = e.company.Id;

        t.GetComponent<CompanyPreviewView>()
            .SetEntity(e);
    }
}
