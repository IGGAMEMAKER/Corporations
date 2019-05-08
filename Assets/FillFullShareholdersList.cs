using Assets.Utils;
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
            .SetItems(shareholders.ToArray());
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        if (screenMode == ScreenMode.InvesmentsScreen)
            Render();
    }
}
