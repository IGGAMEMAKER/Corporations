using Assets.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarketMapRenderer : View
{
    Dictionary<NicheType, GameObject> niches = new Dictionary<NicheType, GameObject>();
    public GameObject NichePrefab;
    public GameObject IndustryPrefab;

    public float IndustrialRadius = 250f;
    public float NicheRadius = 125f;

    public Vector3 BaseOffset = new Vector3(0, 0, 0);

    public override void ViewRender()
    //public void Start()
    {
        //base.ViewRender();

        Render();
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

        

        //var industry = Instantiate(IndustryPrefab, transform, true);
        var baseForIndustry = GetPointPositionOnCircle(j, industriesCount, IndustrialRadius);

        for (var i = 0; i < markets.Length; i++)
        {
            RenderMarket(markets[i], i, markets.Length, baseForIndustry + BaseOffset);
        }
    }

    Vector3 GetPointPositionOnCircle(int index, int length, float radius)
    {
        var angle = index * Mathf.PI * 2 / length;

        return new Vector3(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
    }

    void RenderMarket(GameEntity market, int index, int marketCount, Vector3 basePosition)
    {
        var niche = market.niche.NicheType;

        GameObject m;

        if (!niches.ContainsKey(niche)) 
            niches[niche] = Instantiate(NichePrefab, transform, true);

        m = niches[niche];

        m.transform.position = GetPointPositionOnCircle(index, marketCount, NicheRadius) + basePosition;

        var scale = 1;
        m.transform.localScale = new Vector3(scale, scale, scale);

        m.GetComponent<MarketShareView>().SetEntity(niche);
    }
}
