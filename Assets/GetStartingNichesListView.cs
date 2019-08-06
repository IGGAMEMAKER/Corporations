using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetStartingNichesListView : ListView
{
    public GameObject TypeCorporationNameContainer, ChooseInitialNicheContainer;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        var niche = entity as GameEntity;
        var preview = t.GetComponent<NichePreview>();
        preview.SetNiche(niche, true);

        Destroy(preview.GetComponentInChildren<LinkToNiche>());
        preview.gameObject.AddComponent<SetInitialNiche>().SetNiche(niche.niche.NicheType, TypeCorporationNameContainer, ChooseInitialNicheContainer);
    }

    void Start()
    {
        base.ViewRender();

        var niches = new GameEntity[1];

        niches[0] = NicheUtils.GetNicheEntity(GameContext, NicheType.Forums);

        SetItems(niches);
    }
}
