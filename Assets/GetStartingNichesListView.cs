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

        return profile.AppComplexity == AppComplexity.Easy && profile.AudienceSize != AudienceSize.Global;
    }



    void Start()
    {
        var niches = NicheUtils.GetNiches(GameContext);

        //var niches = NicheUtils.GetPlayableNiches(GameContext)
        //    .Where(IsAppropriateStartNiche)
        //    .ToArray();

        SetItems(niches);
    }
}
