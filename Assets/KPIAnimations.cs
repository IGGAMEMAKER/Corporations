using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KPIAnimations : View
{
    public Text LoyaltyText;
    public Text BugText;
    public Text ClientText;
    public Text ExpertiseText;

    public AnimationSpawner LoyaltyAnimationSpawner;
    public AnimationSpawner BugAnimationSpawner => LoyaltyAnimationSpawner;
    public AnimationSpawner ClientAnimationSpawner => LoyaltyAnimationSpawner;
    public AnimationSpawner ExpertiseAnimationSpawner => LoyaltyAnimationSpawner;

    public override void ViewRender()
    {
        base.ViewRender();

        var loyalty = Marketing.GetSegmentLoyalty(Flagship, 0);

        //LoyaltyAnimationSpawner.SpawnJumpingAnimation(Loyalty.transform);

        PositiveChange(LoyaltyAnimationSpawner, "+2", LoyaltyText);
        PositiveChange(BugAnimationSpawner, "-1", BugText);
        PositiveChange(ClientAnimationSpawner, "+3.3K", ClientText);
        PositiveChange(ExpertiseAnimationSpawner, "+1", ExpertiseText);

        //Animate(loyalty.ToString(), LoyaltyText);
    }

    void PositiveChange(AnimationSpawner spawner, string change, Text text)
    {
        spawner.Spawn(Visuals.Positive(change), text, text.transform);
    }
}
