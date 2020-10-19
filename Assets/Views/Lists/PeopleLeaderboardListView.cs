using Assets.Core;
using UnityEngine;

public class PeopleLeaderboardListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
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
        var people = Investments.GetInfluencialPeople(Q);

        SetItems(people);
    }
}
