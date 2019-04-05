using Assets.Utils;
using Entitas;
using System;

public class FillFriendlyNiches : View
{
    GameEntity[] GetFriendlyNiches(NicheType niche)
    {
        return
            GameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Niche));

        return Array.FindAll(
            GameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Niche)),
            c => c.product.Niche == niche
            );
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
