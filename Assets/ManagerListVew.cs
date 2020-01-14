using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerListVew : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<HumanPreview>().SetEntity((int)(object)entity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var managers = SelectedCompany.team.Managers.Keys;

        SetItems(managers);
    }
}
