using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<TraitView>().SetEntity((Trait)(object)entity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var human = SelectedHuman;

        SetItems(human.humanSkills.Traits);
    }
}
