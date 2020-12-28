using Assets.Core;
using System.Collections.Generic;
using System.Linq;
using Michsky.UI.Frost;
using UnityEngine;

public class DirectCompetitorsListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<CompanyViewOnAudienceMap>().SetEntity(entity as GameEntity);
    }
    public override void ViewRender()
    {
        base.ViewRender();
        
        if (Flagship.isRelease)
        {
            var competitors = Companies.GetDirectCompetitors(Flagship, Q, true)
                .OrderByDescending(c => Companies.IsDirectlyRelatedToPlayer(Q, c) ? 1 : 0); 
            SetItems(competitors);
                // .OrderByDescending(c => c.hasProduct ? Marketing.GetUsers(c) : Economy.CostOf(c, Q)));    
        }
        else
        {
            SetItems(new List<GameEntity> { Flagship });
        }
    }
}