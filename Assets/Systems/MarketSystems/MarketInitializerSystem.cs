using Assets.Utils;
using Entitas;
using System;
using System.Collections.Generic;
using UnityEngine;


public partial class MarketInitializerSystem : IInitializeSystem
{
    readonly GameContext GameContext;

    public MarketInitializerSystem(Contexts contexts)
    {
        GameContext = contexts.game;
    }

    void IInitializeSystem.Initialize()
    {
        InitializeIndustries();

        InitializeFundamentalIndustry();
        InitializeCommunicationsIndustry();
        InitializeEntertainmentIndustry();
        InitializeFinanceIndustry();

        //CheckIndustriesWithZeroNiches();
    }
}

public partial class MarketInitializerSystem : IInitializeSystem
{
    //void CheckIndustriesWithZeroNiches()
    //{
    //    foreach (IndustryType industry in (IndustryType[])Enum.GetValues(typeof(IndustryType)))
    //    {
    //        if (NicheUtils.GetNichesInIndustry(industry, GameContext).Length == 0)
    //            Debug.LogWarning("Industry " + industry.ToString() + " has zero niches! Fill it!");
    //    }
    //}

    void InitializeIndustries()
    {
        foreach (IndustryType industry in (IndustryType[])Enum.GetValues(typeof(IndustryType)))
            GameContext.CreateEntity().AddIndustry(industry);
    }

    GameEntity SetNicheCosts(GameEntity e, float newBasePrice, long newClientBatch, int newTechCost, int newIdeaCost, int newMarketingCost, int newAdCost)
    {
        e.ReplaceNicheCosts(newBasePrice, newClientBatch, newTechCost, newIdeaCost, newMarketingCost, newAdCost);

        return e;
    }

    GameEntity SetNicheCosts(NicheType niche, float newBasePrice, long newClientBatch, int newTechCost, int newIdeaCost, int newMarketingCost, int newAdCost)
    {
        var e = GetNicheEntity(niche);

        return SetNicheCosts(e, newBasePrice, newClientBatch, newTechCost, newIdeaCost, newMarketingCost, newAdCost);
    }

    GameEntity GetNicheEntity(NicheType nicheType)
    {
        return NicheUtils.GetNicheEntity(GameContext, nicheType);
    }

    GameEntity AttachNicheToIndustry(NicheType niche, IndustryType industry)
    {
        var e = GetNicheEntity(niche);

        e.ReplaceNiche(
            e.niche.NicheType,
            industry,
            e.niche.MarketCompatibilities,
            e.niche.CompetingNiches,
            e.niche.Parent
            );



        return e;
    }

    void AttachNichesToIndustry(IndustryType industry, NicheType[] nicheTypes)
    {
        foreach (var n in nicheTypes)
            AttachNicheToIndustry(n, industry);
    }




    GameEntity ForkNiche(NicheType parent, NicheType child)
    {
        var e = GetNicheEntity(child);

        e.ReplaceNiche(
            e.niche.NicheType,
            e.niche.IndustryType,
            e.niche.MarketCompatibilities,
            e.niche.CompetingNiches,
            parent
            );

        return e;
    }

    void AddSynergicNiche(GameEntity entity, NicheType niche, int compatibility)
    {
        var list = entity.niche.MarketCompatibilities;
        var n = entity.niche;

        list.Add(new MarketCompatibility { Compatibility = compatibility, NicheType = niche });

        entity.ReplaceNiche(n.NicheType, n.IndustryType, list, n.CompetingNiches, n.Parent);
    }

    void SetNichesAsSynergic(NicheType niche1, NicheType niche2, int compatibility)
    {
        var n1 = GetNicheEntity(niche1);
        var n2 = GetNicheEntity(niche2);

        AddSynergicNiche(n1, niche2, compatibility);
        AddSynergicNiche(n2, niche1, compatibility);
    }
}
