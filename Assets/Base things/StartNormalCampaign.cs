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
