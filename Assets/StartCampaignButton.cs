using Assets.Utils;
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

        var company = Companies.GenerateCompanyGroup(GameContext, Input.text);

        var startCapital = Markets.GetStartCapital(NicheType, GameContext);

        Companies.SetResources(company, new Assets.Classes.TeamResource(startCapital));

        var niche = Markets.GetNiche(GameContext, NicheType);
        //niche.AddResearch(1);

        Companies.PlayAs(company, GameContext);
        Companies.AutoFillShareholders(GameContext, company, true);

        ScreenUtils.Navigate(GameContext, ScreenMode.NicheScreen, Constants.MENU_SELECTED_NICHE, NicheType);


        SceneManager.UnloadSceneAsync(2);
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    public void SetNiche(NicheType nicheType, InputField Input)
    {
        NicheType = nicheType;
        this.Input = Input;
    }

    public void SetIndustry(IndustryType industry, InputField Input)
    {
        var niches = Markets.GetPlayableNichesInIndustry(industry, GameContext).Where(m => Markets.IsAppropriateStartNiche(m, GameContext)).ToArray();
        var index = Random.Range(0, niches.Count());
        var niche = niches[index].niche.NicheType;

        SetNiche(niche, Input);
    }
}
