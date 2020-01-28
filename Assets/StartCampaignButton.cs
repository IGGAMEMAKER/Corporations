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

        var company = Companies.GenerateCompanyGroup(GameContext, Input.text);
        company.ReplaceCorporateCulture(new System.Collections.Generic.Dictionary<CorporatePolicy, int>
        {
            [CorporatePolicy.BuyOrCreate] = Balance.CORPORATE_CULTURE_LEVEL_MAX,
            [CorporatePolicy.Focusing] = 1,
            [CorporatePolicy.LeaderOrTeam] = 1,
            [CorporatePolicy.WorkerMindset] = Balance.CORPORATE_CULTURE_LEVEL_MAX
        });

        var startCapital = Markets.GetStartCapital(NicheType, GameContext);

        Companies.SetResources(company, new TeamResource(startCapital));

        var niche = Markets.GetNiche(GameContext, NicheType);
        //niche.AddResearch(1);

        Companies.PlayAs(company, GameContext);
        Companies.AutoFillShareholders(GameContext, company, true);

        PrepareMarket(niche, startCapital);


        ScreenUtils.Navigate(GameContext, ScreenMode.NicheScreen, Balance.MENU_SELECTED_NICHE, NicheType);


        SceneManager.UnloadSceneAsync(2);
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    void PrepareMarket(GameEntity niche, long startCapital)
    {
        // spawn competitors
        for (var i = 0; i < 1; i++)
        {
            var c = Markets.SpawnCompany(niche, GameContext, Random.Range(2, 5) * startCapital);

            //MarketingUtils.AddClients(c, MarketingUtils.GetClients(c) * Random.Range(1, 1.5f));
            Marketing.AddBrandPower(c, 10);
        }

        // spawn investors
        for (var i = 0; i < 1; i++)
        {
            var fund = Companies.GenerateInvestmentFund(GameContext, RandomUtils.GenerateInvestmentCompanyName(), 500000);
            Companies.AddFocusNiche(niche.niche.NicheType, fund, GameContext);
        }
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
