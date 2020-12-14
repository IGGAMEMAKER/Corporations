using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class MarketDynamicsAnimationController : View
{
    public GameObject AudienceGrowthPrefab;
    public GameObject LoyaltyGrowthPrefab;

    public List<GameEntity> ObservableCompanies;
    
    public override void ViewRender()
    {
        base.ViewRender();
        
        // Debug.Log();
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
        
    }
}
