using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class MarketDynamicsAnimationController : ListView
{
    public GameObject LoyaltyGrowthPrefab;

    public List<GameEntity> ObservableCompanies;
    
    public override void ViewRender()
    {
        base.ViewRender();
        
        RenderLoyaltyChange();
    }

    void RenderLoyaltyChange()
    {
        PlayNextBubbleSound();
    }

    void PlayNextBubbleSound()
    {
        PlaySound(Sound.Bubble1);
    }

    public void SetObservables(List<GameObject> items)
    {
        // SetItems();
    }

    public override void SetItem<T>(Transform t, T entity)
    {
        throw new System.NotImplementedException();
    }
}
