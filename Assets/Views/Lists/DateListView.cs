using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DateListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        var date = (int)(object)entity;

        t.GetComponent<DateView>().SetEntity(date);
        t.GetComponent<MoveToDate>().SetEntity(date);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var date = CurrentIntDate;

        var amountOfDates = 4;

        var start = date - date % amountOfDates;

        var dates = new int[amountOfDates];
        for (int i = 0; i < amountOfDates; i++)
            dates[i] = start + i;

        SetItems(dates);
    }
}
