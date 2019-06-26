using Assets.Utils;
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
        var shareholders = CompanyUtils.GetCompanyShares(SelectedCompany);

        GetComponent<FullShareholdersListView>()
            .SetItems(shareholders.OrderByDescending(s => CompanyUtils.GetAmountOfShares(GameContext, SelectedCompany.company.Id, s.Key)).ToArray());
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, Dictionary<string, object> data)
    {
        if (screenMode == ScreenMode.InvesmentsScreen)
            Render();
    }
}
