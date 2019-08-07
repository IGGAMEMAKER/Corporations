using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewCampaignController : View
{
    NicheType NicheType;
    public StartCampaignButton StartCampaignButton;
    public InputField Input;

    public void SetNiche(NicheType nicheType)
    {
        NicheType = nicheType;

        StartCampaignButton.SetNiche(nicheType, Input);
    }
}
