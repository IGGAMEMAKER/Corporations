using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCampaignController : View
{
    NicheType NicheType;
    public StartCampaignButton StartCampaignButton;

    public void SetNiche(NicheType nicheType)
    {
        NicheType = nicheType;

        StartCampaignButton.SetNiche(nicheType);
    }
}
