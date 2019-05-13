using Assets.Utils;
using Entitas;

public class FillCompetingNichesList : View
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
        NicheType niche = ScreenUtils.GetSelectedNiche(GameContext);

        GameEntity[] entities = GetFriendlyNiches(niche);

        GetComponent<NicheListView>().SetItems(entities);
    }
}
