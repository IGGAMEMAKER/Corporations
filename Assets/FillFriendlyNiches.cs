﻿using Assets.Utils;
using Entitas;

public class FillFriendlyNiches : View
{
    GameEntity[] GetFriendlyNiches(NicheType niche)
    {
        return GameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Niche));
    }

    void OnEnable()
    {
        Render();
    }

    void Render()
    {
        NicheType niche = MenuUtils.GetNiche(GameContext);

        GameEntity[] entities = GetFriendlyNiches(niche);

        GetComponent<NicheListView>().SetItems(entities);
    }
}
