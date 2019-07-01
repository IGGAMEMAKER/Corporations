using Assets.Utils;
using Entitas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleLeaderboardListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var e = entity as GameEntity;

        t.GetComponent<HumanCapitalsTableView>().SetEntity(e);
    }

    private void OnEnable()
    {
        Render();
    }

    public void Render()
    {
        var people = GetInfluencialPeople(GameContext);

        SetItems(people);
    }

    GameEntity[] GetInfluencialPeople(GameContext gameContext)
    {
        var investors = gameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Shareholder, GameMatcher.Human));

        return investors;
    }
}
