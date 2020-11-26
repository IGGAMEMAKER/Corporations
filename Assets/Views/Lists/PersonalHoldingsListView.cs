using Assets.Core;
using UnityEngine;

public class PersonalHoldingsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
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
        return Investments.GetOwnings(SelectedHuman, Q);
        var human = SelectedHuman;

        if (human.hasShareholder)
            return Investments.GetOwnings(human, Q);

        return new GameEntity[0];
    }
}
