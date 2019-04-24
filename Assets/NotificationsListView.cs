using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        throw new System.NotImplementedException();
    }

    void Start()
    {
        Render();
    }

    void Render()
    {
        //var shareholders = GetShareholders();

        //GetComponent<ShareholdersListView>()
        //    .SetItems(shareholders.ToArray());
    }

    //Dictionary<int, int> GetShareholders()
    //{
    //    Dictionary<int, int> shareholders = new Dictionary<int, int>();

    //    if (SelectedCompany.hasShareholders)
    //        shareholders = SelectedCompany.shareholders.Shareholders;

    //    return shareholders;
    //}
}
