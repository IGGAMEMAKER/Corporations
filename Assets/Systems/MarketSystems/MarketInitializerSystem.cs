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
        InitializeNiches();

        //InitializeOSIndustry();
        InitializeSearchIndustry();
        InitializeCommunicationsIndustry();

        CheckIndustriesWithZeroNiches();
    }
}

public partial class MarketInitializerSystem : IInitializeSystem
{
    void InitializeNiches()
    {
        foreach (NicheType niche in (NicheType[])Enum.GetValues(typeof(NicheType)))
        {
            //if (niche == NicheType.None) continue;

            var e = GameContext.CreateEntity();

            e.AddNiche(
                niche,
                IndustryType.Communications,
                new List<MarketCompatibility>(),
                new List<NicheType>(),
                NicheType.SocialNetwork,
                0
                );

            e.AddNicheCosts(1, 1, 1, 1, 1, 1);
            e.AddNicheState(
                new Dictionary<NicheLifecyclePhase, int>
                {
                    [NicheLifecyclePhase.Idle] = 0, // 0
                    [NicheLifecyclePhase.Innovation] = UnityEngine.Random.Range(1, 4), // 2-5            Xt
                    [NicheLifecyclePhase.Trending] = UnityEngine.Random.Range(5, 10), // 4 - 10           5Xt
                    [NicheLifecyclePhase.MassUse] = UnityEngine.Random.Range(11, 15), // 7 - 15            10Xt
                    [NicheLifecyclePhase.Decay] = UnityEngine.Random.Range(2, 5), // 2 - 5 // churn      3Xt-22Xt
                    [NicheLifecyclePhase.Death] = 0, // churn
                },
                NicheLifecyclePhase.Innovation,
                0
                );

            e.AddSegment(new Dictionary<UserType, int>
            {
                [UserType.Core] = 1,
                [UserType.Regular] = 1,
                [UserType.Mass] = 1,
            });
        }
    }

    void CheckIndustriesWithZeroNiches()
    {
        foreach (IndustryType industry in (IndustryType[])Enum.GetValues(typeof(IndustryType)))
        {
            if (NicheUtils.GetNichesInIndustry(industry, GameContext).Length == 0)
                Debug.LogWarning("Industry " + industry.ToString() + " has zero niches! Fill it!");
        }
    }

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
            e.niche.Parent,
            e.niche.OpenDate
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
            parent,
            e.niche.OpenDate
            );

        return e;
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

        entity.ReplaceNiche(n.NicheType, n.IndustryType, list, n.CompetingNiches, n.Parent, n.OpenDate);
    }

    void SetNichesAsSynergic(NicheType niche1, NicheType niche2, int compatibility)
    {
        var n1 = GetNicheEntity(niche1);
        var n2 = GetNicheEntity(niche2);

        AddSynergicNiche(n1, niche2, compatibility);
        AddSynergicNiche(n2, niche1, compatibility);
    }
}
