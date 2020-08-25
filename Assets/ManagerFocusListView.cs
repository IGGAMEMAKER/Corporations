using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerFocusListView : ListView
{
    public GameObject FocusList;
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<ManagerTaskSlotView>().SetEntity(index);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        List<ManagerTask> tasks = new List<ManagerTask> { ManagerTask.Documentation, ManagerTask.Investments, ManagerTask.None };

        SetItems(tasks);
        Hide(FocusList);
    }

    public override void OnItemSelected(int ind)
    {
        base.OnItemSelected(ind);

        Debug.Log("On manager focus selected");

        Show(FocusList);
    }

    public override void OnDeselect()
    {
        base.OnDeselect();

        Hide(FocusList);
    }
}
