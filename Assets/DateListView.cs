using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var date = (int)(object)entity;

        t.GetComponent<DateView>().SetEntity(date);
        t.GetComponent<MoveToDate>().SetEntity(date);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var date = CurrentIntDate;

        var dateOffset = 2;
        var amountOfDates = dateOffset * 2 + 1;

        var start = date - date % 7;

        var dates = new int[amountOfDates];
        for (int i = 0; i < amountOfDates; i++)
            dates[i] = start + i;

        SetItems(dates);
    }
}
