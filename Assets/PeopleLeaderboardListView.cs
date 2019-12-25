using Assets.Utils;
using UnityEngine;

public class PeopleLeaderboardListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var e = entity as GameEntity;

        t.GetComponent<HumanCapitalsTableView>().SetEntity(e, t.GetSiblingIndex());
    }

    private void OnEnable()
    {
        Render();
    }

    public void Render()
    {
        var people = Investments.GetInfluencialPeople(GameContext);

        SetItems(people);
    }
}
