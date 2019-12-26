using Assets.Core;
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

    void Start()
    {
        //var niches = NicheUtils.GetNiches(GameContext);

        var niches = Markets.GetPlayableNiches(GameContext)
            .Where(n => Markets.IsAppropriateStartNiche(n, GameContext))
            ;

        SetItems(niches.ToArray());
    }
}
