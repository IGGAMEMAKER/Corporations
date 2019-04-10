using Assets.Utils;
using Entitas;
using System;
using System.Linq;
using UnityEngine;

public class FillCompetingCompaniesList : View
    , IMenuListener
    , IAnyCompanyListener
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

    void OnEnable()
    {
        GetUniversalListener.AddAnyCompanyListener(this);
    }

    void IMenuListener.OnMenu(GameEntity entity, ScreenMode screenMode, object data)
    {
        if (screenMode == ScreenMode.NicheScreen)
            Render();
    }

    int SortCompanies(GameEntity p1, GameEntity p2)
    {
        if (p1.isControlledByPlayer)
            return -1;

        if (p2.isControlledByPlayer)
            return 1;

        if (p1.product.ProductLevel == 0)
            return -1;

        if (p2.product.ProductLevel == 0)
            return 1;

        return 0;
    }

    void Render()
    {
        NicheType niche = MenuUtils.GetNiche(GameContext);

        GameEntity[] entities = GetProductsOnNiche(niche);

        Array.Sort(entities, SortCompanies);

        string names = String.Join(",", entities.Select(e => e.product.Name).ToArray());

        Debug.Log("Rendering companies: " + names);

        GetComponent<CompetingCompaniesListView>().SetItems(entities);
    }

    void IAnyCompanyListener.OnAnyCompany(GameEntity entity, int id, string name, CompanyType companyType)
    {
        Render();
    }
}
