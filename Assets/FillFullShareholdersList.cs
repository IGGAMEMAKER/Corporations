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
        var shareholders = GetShareholders();

        GetComponent<FullShareholdersListView>()
            .SetItems(shareholders.ToArray());
    }

    Dictionary<int, int> GetShareholders()
    {
        Dictionary<int, int> shareholders = new Dictionary<int, int>();

        var companyEntity = SelectedCompany;

        if (companyEntity.hasShareholders)
            shareholders = companyEntity.shareholders.Shareholders;

        return shareholders;
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        if (screenMode == ScreenMode.InvesmentsScreen)
            Render();
    }
}
