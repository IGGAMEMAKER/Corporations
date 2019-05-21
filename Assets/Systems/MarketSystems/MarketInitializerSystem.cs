using Assets.Utils;
using Entitas;
using System;
using System.Collections.Generic;
using UnityEngine;

public partial class MarketInitializerSystem : IInitializeSystem
    //, IMarketGenerator
{
    readonly GameContext GameContext;

    public MarketInitializerSystem(Contexts contexts)
    {
        GameContext = contexts.game;
    }

    void CheckIndustriesWithZeroNiches()
    {
        foreach (IndustryType industry in (IndustryType[])Enum.GetValues(typeof(IndustryType)))
        {
            if (NicheUtils.GetNichesInIndustry(industry, GameContext).Length == 0)
                Debug.LogWarning("Industry " + industry.ToString() + " has zero niches! Fill it!");
        }
    }

    void IInitializeSystem.Initialize()
    {
        InitializeIndustries();
        InitializeNiches();

        InitializeSearchIndustry();
        InitializeOSIndustry();
        InitializeCommunicationsIndustry();
        InitializeCloudsIndustry();

        CheckIndustriesWithZeroNiches();
    }

    void InitializeIndustries()
    {
        foreach (IndustryType industry in (IndustryType[])Enum.GetValues(typeof(IndustryType)))
            GameContext.CreateEntity().AddIndustry(industry);
    }

    void InitializeNiches()
    {
        foreach (NicheType niche in (NicheType[])Enum.GetValues(typeof(NicheType)))
        {
            if (niche == NicheType.None) continue;

            var e = GameContext.CreateEntity();

            e.AddNiche(
                niche,
                IndustryType.CloudComputing,
                new List<MarketCompatibility>(),
                new List<NicheType>(),
                NicheType.None,
                0,
                10f
                );
        }
    }

    GameEntity GetNicheEntity(NicheType nicheType)
    {
        return Array.Find(GameContext.GetEntities(GameMatcher.Niche), n => n.niche.NicheType == nicheType);
    }

    void AttachNicheToIndustry(NicheType niche, IndustryType industry)
    {
        var e = GetNicheEntity(niche);

        e.ReplaceNiche(
            e.niche.NicheType,
            industry,
            e.niche.MarketCompatibilities,
            e.niche.CompetingNiches,
            e.niche.Parent,
            e.niche.OpenDate,
            e.niche.BasePrice
            );
    }

    void ForkNiche(NicheType parent, NicheType child)
    {
        var e = GetNicheEntity(child);

        e.ReplaceNiche(
            e.niche.NicheType,
            e.niche.IndustryType,
            e.niche.MarketCompatibilities,
            e.niche.CompetingNiches,
            parent,
            e.niche.OpenDate,
            e.niche.BasePrice
            );
    }

    void SetChildsAsCompetingNiches(NicheType parent)
    {
        
    }

    void SetNicheAsDependant(NicheType niche, NicheType sourceNiche, int dependency)
    {
        
    }

    void AddSynergicNiche(GameEntity entity, NicheType niche, int compatibility)
    {
        var list = entity.niche.MarketCompatibilities;
        var n = entity.niche;

        list.Add(new MarketCompatibility { Compatibility = compatibility, NicheType = niche });

        entity.ReplaceNiche(n.NicheType, n.IndustryType, list, n.CompetingNiches, n.Parent, n.OpenDate, n.BasePrice);
    }

    void SetNichesAsSynergic(NicheType niche1, NicheType niche2, int compatibility)
    {
        var n1 = GetNicheEntity(niche1);
        var n2 = GetNicheEntity(niche2);

        AddSynergicNiche(n1, niche2, compatibility);
        AddSynergicNiche(n2, niche1, compatibility);
    }
}
