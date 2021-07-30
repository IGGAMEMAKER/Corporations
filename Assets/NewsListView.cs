using Assets.Visuals;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NewsListView : ListView
{
    List<string> Messages = new List<string>();

    public override void SetItem<T>(Transform t, T entity)
    {
        var message = entity as string;
        //Debug.Log("ITEM: " + message);

        t.GetComponent<MockText>().SetEntity(message);

        if (index == 0)
            AddIfAbsent<EnlargeOnAppearance>(t.gameObject);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        if (CurrentIntDate % 30 == 0)
        {
            var message = "You got message " + Random.Range(0, 10);

            Messages.Insert(0, message);

            SetItems(Messages.Take(3));
        }
    }
}
