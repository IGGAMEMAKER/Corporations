using Assets.Core;
using System.Collections.Generic;
using System.Linq;

public class FillFullShareholdersList : View, IMenuListener
{
    void Start()
    {
        ListenMenuChanges(this);

        Render();
    }

    void Render()
    {
        var shareholders = Companies.GetShareholders(SelectedCompany);

        GetComponent<FullShareholdersListView>()
            .SetItems(shareholders.OrderByDescending(s => Companies.GetAmountOfShares(GameContext, SelectedCompany.company.Id, s.Key)).ToArray());
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, Dictionary<string, object> data)
    {
        if (screenMode == ScreenMode.InvesmentsScreen)
            Render();
    }
}
