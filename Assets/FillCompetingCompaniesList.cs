using Assets.Utils;
using Entitas;
using System;
using System.Linq;
using UnityEngine;

public class FillCompetingCompaniesList : View, IMenuListener
{
    GameEntity[] GetProductsOnNiche(NicheType niche)
    {
        Debug.Log("GetProductsOnNiche " + niche.ToString());

        return Array.FindAll(
            GameContext.GetEntities(GameMatcher.AllOf(GameMatcher.Company, GameMatcher.Product)),
            c => c.product.Niche == niche
            );
    }

    void Start()
    {
        ListenMenuChanges(this);

        Render();
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        if (screenMode == ScreenMode.NicheScreen)
        {
            Render();
        }
    }

    void Render()
    {
        NicheType niche = MenuUtils.GetNiche(GameContext);

        GameEntity[] entities = GetProductsOnNiche(niche);

        string names = String.Join(",", entities.Select(e => e.product.Name).ToArray());

        Debug.Log("Rendering companies: " + names);

        GetComponent<CompetingCompaniesListView>().SetItems(entities);
    }
}
