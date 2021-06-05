using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNewCampaignFromExitModal : ButtonController
{
    public override void Execute()
    {
        State.ClearEntities();
        State.StartNewCampaign(Q, NicheType.Com_SocialNetwork, "QQQ");

        //State.LoadGameData(Q);
        //State.LoadGameScene();

        CloseModal("Exit");
    }
}
