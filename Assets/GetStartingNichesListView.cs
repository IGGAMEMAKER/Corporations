using Assets.Utils;
using System;
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

        var isGlobalMarket = profile.AudienceSize == AudienceSize.Global;
        var isConceptLevelLow = true || Products.GetMarketDemand(niche) < 10;

        var isPerspective = Markets.IsPerspectiveNiche(niche);
        var isCheapToMaintain = profile.AppComplexity < AppComplexity.Hard;

        return isPerspective && isCheapToMaintain;
        return isCheapToMaintain && !isGlobalMarket && isPerspective && isConceptLevelLow;
    }



    void Start()
    {
        //var niches = NicheUtils.GetNiches(GameContext);

        var niches = Markets.GetPlayableNiches(GameContext)
            .Where(IsAppropriateStartNiche)
            ;

        SetItems(niches.ToArray());
    }
}
