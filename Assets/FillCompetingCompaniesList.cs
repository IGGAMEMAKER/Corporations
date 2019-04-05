using Assets.Utils;
using Entitas;
using System;
using UnityEngine;

public class FillCompetingCompaniesList : View, IMenuListener
{
    public CompetingCompaniesListView CompetingCompaniesListView;

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
    }

    void OnEnable()
    {
        Debug.Log("OnEnable FillCompetingCompaniesList");

        Render();
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        if (screenMode == ScreenMode.NicheScreen)
            Render();
    }

    void Render()
    {
        NicheType niche = MenuUtils.GetNiche(GameContext);

        GameEntity[] entities = GetProductsOnNiche(niche);

        Debug.Log("Will print " + entities.Length + " products");

        CompetingCompaniesListView.SetItems(entities);
    }
}
