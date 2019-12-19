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

        // spawn competitors
        for (var i = 0; i < 1; i++)
        {
            var c = Markets.SpawnCompany(niche, GameContext, Random.Range(2, 5) * startCapital);

            //MarketingUtils.AddClients(c, MarketingUtils.GetClients(c) * Random.Range(1, 1.5f));
            MarketingUtils.AddBrandPower(c, 10);
        }

        company.ReplaceCorporateCulture(new System.Collections.Generic.Dictionary<CorporatePolicy, int>
        {
            [CorporatePolicy.CreateOrBuy] = 4,
            [CorporatePolicy.Focusing] = 1,
            [CorporatePolicy.LeaderOrTeam] = 1,
            [CorporatePolicy.WorkerMindset] = 2
        });

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
