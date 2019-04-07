using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FillFullShareholdersList : View, IMenuListener
{
    void Start()
    {
        ListenMenuChanges(this);

        Render();
    }

    void Render()
    {
        var shareholders = GetShareholders();

        GetComponent<FullShareholdersListView>()
            .SetItems(shareholders.ToArray());
    }

    Dictionary<int, int> GetShareholders()
    {
        Dictionary<int, int> shareholders = new Dictionary<int, int>();

        var companyEntity = myProductEntity;

        if (companyEntity.hasShareholders)
            shareholders = companyEntity.shareholders.Shareholders;

        return shareholders;
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        if (screenMode == ScreenMode.ProjectScreen)
            Render();
    }
}
