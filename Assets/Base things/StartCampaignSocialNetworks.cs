using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCampaignSocialNetworks : CampaignStarter
{
    public override void Execute2(string name)
    {
        State.StartNewCampaign(Q, NicheType.Com_SocialNetwork, name);
    }
}

public abstract class CampaignStarter : ButtonController
{
    public override void Execute()
    {
        //FindObjectOfType<StartCampaignButton>().CampaignButton = gameObject;

        FindObjectOfType<StartNormalCampaign>().Execute();
    }

    public abstract void Execute2(string name);
}