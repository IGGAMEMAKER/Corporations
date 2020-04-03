using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpertiseListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<ExpertiseView>().SetEntity((NicheType)(object)entity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        SetItems(SelectedHuman.humanSkills.Expertise.Keys);
    }
}
