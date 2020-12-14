using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DirectCompetitorsListView : ListView
{
    private MarketDynamicsAnimationController AnimationController;
    
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<CompanyViewOnAudienceMap>().SetEntity(entity as GameEntity);
    }

    public override void ViewRender()
    {
        base.ViewRender();
        
        if (Flagship.isRelease)
            SetItems(Companies.GetDirectCompetitors(Flagship, Q, true).OrderByDescending(c => c.hasProduct ? Marketing.GetUsers(c) : Economy.CostOf(c, Q)));
        else
        {
            SetItems(new List<GameEntity> { Flagship });
        }
    }

    void Start()
    {
        AnimationController = FindObjectOfType<MarketDynamicsAnimationController>();
    }

    public override void OnListChanged()
    {
        base.OnListChanged();

        AnimationController.SetObservables(Items);
    }
}
