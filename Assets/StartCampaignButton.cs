using Assets.Core;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartCampaignButton : ButtonController
{
    NicheType NicheType;
    //IndustryType Industry;
    InputField Input;

    public override void Execute()
    {
        if (Input.text.Length == 0)
            return;

        ScreenUtils.StartNewCampaign(Q, NicheType, Input.text);
    }

    public void SetNiche(NicheType nicheType, InputField Input)
    {
        NicheType = nicheType;
        this.Input = Input;
    }

    public void SetIndustry(IndustryType industry, InputField Input)
    {
        var niches = Markets.GetPlayableNichesInIndustry(industry, Q).Where(m => Markets.IsAppropriateStartNiche(m, Q)).ToArray();
        var index = Random.Range(0, niches.Count());
        var niche = niches[index].niche.NicheType;

        SetNiche(niche, Input);
    }
}
