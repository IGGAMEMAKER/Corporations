using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NicheMapRenderer : View
{
    Dictionary<NicheType, GameObject> niches = new Dictionary<NicheType, GameObject>();
    Dictionary<int, GameObject> companies = new Dictionary<int, GameObject>();

    public GameObject NichePrefab;
    public GameObject CompanyPrefab;

    [HideInInspector]
    internal float NicheRadius = 225f;
    [HideInInspector]
    internal float CompanyRadius = 85f;

    public override void ViewRender()
    {
        Render();
    }

    void Render()
    {
        var baseForIndustry = new Vector3(350, -350); // Rendering.GetPointPositionOnCircle(0, 1, 0, 1);

        RenderMarket(SelectedNiche, 0, 1, baseForIndustry);
    }

    void RenderMarket(NicheType niche, int index, int marketCount, Vector3 industryPosition)
    {
        GameObject m = GetMarketObject(niche);

        UpdateMarketPosition(m, index, marketCount, industryPosition, niche);

        m.GetComponent<MarketShareView>().SetEntity(niche);

        if (!NicheUtils.IsPlayableNiche(GameContext, niche))
            m.SetActive(false);

        RenderCompanies(niche, m);
    }

    void RenderCompanies(NicheType niche, GameObject m)
    {
        var competitors = NicheUtils.GetPlayersOnMarket(GameContext, niche, true);

        var marketPosition = m.transform.localPosition;

        var count = competitors.Length;
        for (var i = 0; i < count; i++)
            RenderCompany(competitors[i], i, count, marketPosition);
    }

    void RenderCompany(GameEntity company, int index, int amount, Vector3 marketPosition)
    {
        GameObject c = GetCompanyObject(company.company.Id);

        UpdateCompanyPosition(c, index, amount, marketPosition, company.product.Niche);

        c.GetComponent<CompanyViewOnMap>().SetEntity(company, true);
    }

    GameObject GetMarketObject(NicheType niche)
    {
        if (!niches.ContainsKey(niche))
            niches[niche] = Instantiate(NichePrefab, transform);

        return niches[niche];
    }

    GameObject GetCompanyObject(int companyId)
    {
        if (!companies.ContainsKey(companyId))
            companies[companyId] = Instantiate(CompanyPrefab, transform);

        return companies[companyId];
    }



    float GetMarketScale(NicheType niche)
    {
        var marketSize = NicheUtils.GetMarketSize(GameContext, niche);

        if (marketSize < 0)
            marketSize = 1000;

        return Mathf.Clamp(Mathf.Log10(marketSize) / 4f, 0.8f, 2.5f);
    }


    // set transforms
    void UpdateMarketPosition(GameObject m, int index, int marketCount, Vector3 basePosition, NicheType niche)
    {
        var scale = GetMarketScale(niche);

        m.transform.localScale = new Vector3(scale, scale, 1);
        m.transform.localPosition = Rendering.GetPointPositionOnCircle(index, marketCount, NicheRadius, 1) + basePosition;
    }

    void UpdateCompanyPosition(GameObject c, int index, int count, Vector3 basePosition, NicheType niche)
    {
        var scale = 0.75f; // GetMarketScale(niche);

        var marketScale = GetMarketScale(niche) - 0.8f;

        c.transform.localScale = new Vector3(scale, scale, 1);
        c.transform.localPosition = Rendering.GetPointPositionOnCircle(index, count, CompanyRadius + marketScale * 25f, 1, Mathf.PI) + basePosition;
    }
}
