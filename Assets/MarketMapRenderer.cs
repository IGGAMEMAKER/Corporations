using Assets.Utils;
using System.Collections.Generic;
using UnityEngine;

public class MarketMapRenderer : View
{
    Dictionary<NicheType, GameObject> niches = new Dictionary<NicheType, GameObject>();
    public GameObject NichePrefab;
    public GameObject IndustryPrefab;

    public float IndustrialRadius = 250f * 1.75f;
    public float NicheRadius = 125f * 2.5f;

    public Vector3 BaseOffset = new Vector3(0, 0, 0);

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
        
        var baseForIndustry = GetPointPositionOnCircle(j, industriesCount, IndustrialRadius);

        for (var i = 0; i < markets.Length; i++)
            RenderMarket(markets[i], i, markets.Length, baseForIndustry + BaseOffset);
    }

    Vector3 GetPointPositionOnCircle(int index, int length, float radius)
    {
        var angle = index * Mathf.PI * 2 / length;

        return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * radius * Zoom;
    }

    void RenderMarket(GameEntity market, int index, int marketCount, Vector3 basePosition)
    {
        var niche = market.niche.NicheType;

        GameObject m = GetMarketObject(niche);

        UpdateMarketPosition(m, index, marketCount, basePosition, niche);

        m.GetComponent<MarketShareView>().SetEntity(niche);
    }

    GameObject GetMarketObject(NicheType niche)
    {
        if (!niches.ContainsKey(niche))
            niches[niche] = Instantiate(NichePrefab, transform, false);

        return niches[niche];
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
        var scale = GetMarketScale(niche) * Zoom;

        m.transform.localScale = new Vector3(scale, scale, 1);
        m.transform.position = GetPointPositionOnCircle(index, marketCount, NicheRadius) + basePosition;
    }
}
