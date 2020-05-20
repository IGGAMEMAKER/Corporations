using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNormalCampaign : ButtonController
{
    public GameObject ChooseNiche;
    public GameObject ChooseCampaign;

    public override void Execute()
    {
        ChooseNiche.SetActive(true);
        ChooseCampaign.SetActive(false);
    }
}
