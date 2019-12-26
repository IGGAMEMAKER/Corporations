using Assets.Core;
using UnityEngine;

public class PersonalHoldingsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var e = entity as GameEntity;

        t.GetComponent<HumanHoldingsPreview>().SetEntity(e);
    }

    private void OnEnable()
    {
        Render();
    }

    public void Render()
    {
        var holdings = GetInvestments();

        SetItems(holdings);
    }

    private GameEntity[] GetInvestments()
    {
        if (SelectedHuman.hasShareholder)
            return Investments.GetInvestmentsOf(GameContext, SelectedHuman.shareholder.Id);

        return new GameEntity[0];
    }
}
