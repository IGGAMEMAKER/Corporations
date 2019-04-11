using UnityEngine;

public class OwningsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var e = entity as GameEntity;

        t.GetComponent<CompanyPreviewView>()
            .SetEntity(e);
    }
}

public class SelectCompanyController : ButtonController
{
    public int companyId;
    //bool selected;

    public override void Execute()
    {
        //selected = true;
        SetSelectedCompany(companyId);
    }
}
