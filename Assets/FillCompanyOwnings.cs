using Entitas;
using System;

public class FillCompanyOwnings : View
    ,IMenuListener
{
    // Start is called before the first frame update
    void Start()
    {
        ListenMenuChanges(this);

        Render();
    }

    GameEntity[] GetOwnings()
    {
        if (!SelectedCompany.hasShareholder)
            return new GameEntity[0];

        var investableCompanies = GameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Company, GameMatcher.Shareholders));

        int shareholderId = SelectedCompany.shareholder.Id;

        return Array.FindAll(investableCompanies, e => e.shareholders.Shareholders.ContainsKey(shareholderId));
    }

    void Render()
    {
        var ownings = GetOwnings();

        GetComponent<OwningsListView>().SetItems(ownings);
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        if (screenMode == ScreenMode.BusinessScreen)
            Render();
    }
}
