using Assets.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MarketMapRenderer : View
{
    Dictionary<NicheType, GameObject> niches = new Dictionary<NicheType, GameObject>();
    Dictionary<int, GameObject> companies = new Dictionary<int, GameObject>();

    public GameObject NichePrefab;
    public GameObject IndustryPrefab;
    public GameObject CompanyPrefab;

    public float IndustrialRadius = 350f * 1.75f;
    public float NicheRadius = 125f * 2.5f;
    public float CompanyRadius = 50f * 2.5f;

    public Vector3 BaseOffset = new Vector3(425, 250, 0);

    public override void ViewRender()
    //public void Start()
    {
        //base.ViewRender();

        Render();
    }

    MapNavigation mapNavigation;

    float Zoom
    {
        get
        {
            if (mapNavigation == null)
                mapNavigation = GetComponent<MapNavigation>();
            
            return mapNavigation.Zoom;
        }
    }

    void Render()
    {
        var industries = NicheUtils.GetIndustries(GameContext);

        var length = industries.Length;
        for (var i = 0; i < length; i++)
        {
            var ind = industries[i];

            RenderIndustry(ind, i, length);
        }
    }

    private void RenderIndustry(GameEntity ind, int j, int industriesCount)
    {
        var markets = NicheUtils.GetNichesInIndustry(ind.industry.IndustryType, GameContext);

        var baseRadius = IndustrialRadius + NicheRadius;
        var baseMapOffset = new Vector3(baseRadius, -baseRadius);
        var baseForIndustry = GetPointPositionOnCircle(j, industriesCount, IndustrialRadius) + baseMapOffset;

        for (var i = 0; i < markets.Length; i++)
            RenderMarket(markets[i].niche.NicheType, i, markets.Length, baseForIndustry);
            //RenderMarket(markets[i].niche.NicheType, i, markets.Length, baseForIndustry + BaseOffset);
    }

    Vector3 GetPointPositionOnCircle(int index, int length, float radius)
    {
        var angle = index * Mathf.PI * 2 / length;

        return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
    }

    void RenderMarket(NicheType niche, int index, int marketCount, Vector3 industryPosition)
    {
        GameObject m = GetMarketObject(niche);

        UpdateMarketPosition(m, index, marketCount, industryPosition, niche);

        m.GetComponent<MarketShareView>().SetEntity(niche);

        var competitors = NicheUtils.GetPlayersOnMarket(GameContext, niche, true);

        var count = competitors.Count();
        for (var i = 0; i < count; i++)
            RenderCompany(competitors[i], i, count, m.transform.localPosition);
    }



    void RenderCompany(GameEntity company, int index, int amount, Vector3 marketPosition)
    {
        GameObject c = GetCompanyObject(company.company.Id);

        UpdateCompanyPosition(c, index, amount, marketPosition);
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
            companies[companyId] = Instantiate(NichePrefab, transform);

        return companies[companyId];
    }

    float GetMarketScale(NicheType niche)
    {
        var marketSize = NicheUtils.GetMarketSize(GameContext, niche);

        if (marketSize < 0)
            marketSize = 1000;

        return Mathf.Clamp(Mathf.Log10(marketSize) / 4f, 0.8f, 2.5f);
    }

    void UpdateMarketPosition(GameObject m, int index, int marketCount, Vector3 basePosition, NicheType niche)
    {
        var scale = GetMarketScale(niche);

        m.transform.localScale = new Vector3(scale, scale, 1);
        m.transform.localPosition = GetPointPositionOnCircle(index, marketCount, NicheRadius) + basePosition;
    }

    void UpdateCompanyPosition(GameObject m, int index, int count, Vector3 basePosition)
    {
        var scale = 1; // GetMarketScale(niche);

        m.transform.localScale = new Vector3(scale, scale, 1);
        m.transform.localPosition = GetPointPositionOnCircle(index, count, CompanyRadius) + basePosition;
    }
}
