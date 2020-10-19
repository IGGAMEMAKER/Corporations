using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RenderAudiencesOnAudiencePanelListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        //t.GetComponent<>
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var audiences = Marketing.GetAudienceInfos();

        audiences.Take(Flagship.isRelease ? audiences.Count : 1);

        SetItems(audiences);
    }
}
