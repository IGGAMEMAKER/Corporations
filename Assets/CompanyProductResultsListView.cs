using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public struct ProductCompanyResult
{
    public long clientChange;
    public float MarketShareChange;
    public ConceptStatus ConceptStatus;
    public int CompanyId;
}

public class CompanyProductResultsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<CompanyResultView>().SetEntity((ProductCompanyResult)(object)entity);
    }

    ProductCompanyResult GetProductCompanyResults (GameEntity product)
    {
        var competitors = Markets.GetProductsOnMarket(GameContext, product);

        long previousMarketSize = 0;
        long currentMarketSize = 0;

        long prevCompanyClients = 0;
        long currCompanyClients = 0;

        foreach (var c in competitors)
        {
            var last = c.metricsHistory.Metrics.Count - 1;
            var prev = c.metricsHistory.Metrics.Count - 2;

            // company was formed this month
            if (prev < 0)
                continue;

            var audience = c.metricsHistory.Metrics[prev].AudienceSize;
            var clients = c.metricsHistory.Metrics[last].AudienceSize;

            previousMarketSize += audience;
            currentMarketSize += clients;

            if (c.company.Id == product.company.Id)
            {
                prevCompanyClients = audience;
                currCompanyClients = clients;
            }
        }

        var prevShare = prevCompanyClients * 100 / (previousMarketSize + 1);
        var Share = currCompanyClients * 100 / (currentMarketSize + 1);

        return new ProductCompanyResult {
            clientChange = currCompanyClients - prevCompanyClients,
            MarketShareChange = Share - prevShare,
            ConceptStatus = ProductUtils.GetConceptStatus(product, GameContext),
            CompanyId = product.company.Id
        };
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var daughters = Companies.GetDaughterCompanies(GameContext, MyCompany.company.Id);
        var results = daughters
            .Select(GetProductCompanyResults)
            .ToArray();

        SetItems(results);
    }
}
