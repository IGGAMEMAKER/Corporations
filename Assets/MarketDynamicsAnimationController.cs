using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class MarketDynamicsAnimationController : View
{
    public GameObject AudienceGrowthPrefab;
    public GameObject LoyaltyGrowthPrefab;
    
    public override void ViewRender()
    {
        base.ViewRender();
        
        // Debug.Log();
        RenderLoyaltyChange();
    }

    void RenderLoyaltyChange()
    {
        PlaySound(Sound.Bubble1);
    }
}
