using Assets.Utils;
using System.Linq;

public class FillCompanyShareholders : View, IMenuListener
{
    void Start()
    {
        ListenMenuChanges(this);

        Render();
    }

    void Render()
    {
        var shareholders = CompanyUtils.GetCompanyShares(SelectedCompany);

        GetComponent<ShareholdersListView>()
            .SetItems(shareholders.ToArray());
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        if (screenMode == ScreenMode.ProjectScreen)
            Render();
    }
}
