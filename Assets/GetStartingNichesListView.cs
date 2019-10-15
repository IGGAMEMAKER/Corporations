using Assets.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GetStartingNichesListView : ListView
{
    public GameObject TypeCorporationNameContainer, ChooseInitialNicheContainer;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var niche = entity as GameEntity;
        var preview = t.GetComponent<NichePreview>();
        preview.SetNiche(niche, true);

        var link = preview.GetComponentInChildren<LinkToNiche>();
        link.gameObject.AddComponent<SetInitialNiche>()
            .SetNiche(niche.niche.NicheType, TypeCorporationNameContainer, ChooseInitialNicheContainer);

        Destroy(link);
    }

    bool IsAppropriateStartNiche(GameEntity niche)
    {
        var profile = niche.nicheBaseProfile.Profile;

        return profile.AppComplexity == AppComplexity.Easy && profile.AudienceSize != AudienceSize.Global;
    }

    GameEntity ChooseAppropriateMarket(GameEntity[] markets)
    {
        return RandomUtils.RandomItem(markets.Where(IsAppropriateStartNiche));
    }



    GameEntity[] GetStartNichesInIndustry(IndustryType industry, GameContext context)
    {
        var niches = NicheUtils.GetNichesInIndustry(industry, context);

        return Array.FindAll(niches, IsAppropriateStartNiche);
    }

    void Start()
    {
        var niches = NicheUtils.GetPlayableNiches(GameContext)
            .Where(IsAppropriateStartNiche)
            .ToArray();

        var availableEntertainingMarkets = NicheUtils.GetPlayableNichesInIndustry(IndustryType.Entertainment, GameContext);
        var availableCommunicationMarkets = NicheUtils.GetPlayableNichesInIndustry(IndustryType.Communications, GameContext);

        //niches[0] = NicheUtils.GetNicheEntity(GameContext, NicheType.Com_Forums);
        //niches[1] = NicheUtils.GetNicheEntity(GameContext, NicheType.Com_SocialNetwork);

        //niches[0] = ChooseAppropriateMarket(availableEntertainingMarkets);
        //niches[1] = ChooseAppropriateMarket(availableCommunicationMarkets);

        SetItems(niches);
    }
}
