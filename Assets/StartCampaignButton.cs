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

        var company = Companies.GenerateCompanyGroup(Q, Input.text);

        var half = Balance.CORPORATE_CULTURE_LEVEL_MAX / 2;

        company.ReplaceCorporateCulture(new System.Collections.Generic.Dictionary<CorporatePolicy, int>
        {
            [CorporatePolicy.BuyOrCreate] = half,
            [CorporatePolicy.FocusingOrSpread] = 1,
            [CorporatePolicy.LeaderOrTeam] = 1,
            [CorporatePolicy.InnovationOrStability] = half,
            [CorporatePolicy.SalariesLowOrHigh] = half,
            [CorporatePolicy.CompetitionOrSupport] = half
        });

        var startCapital = Markets.GetStartCapital(NicheType, Q);

        Companies.SetResources(company, new TeamResource(startCapital));

        var niche = Markets.GetNiche(Q, NicheType);
        //niche.AddResearch(1);

        Companies.PlayAs(company, Q);
        Companies.AutoFillShareholders(Q, company, true);

        PrepareMarket(niche, startCapital);


        ScreenUtils.Navigate(Q, ScreenMode.NicheScreen, Balance.MENU_SELECTED_NICHE, NicheType);


        SceneManager.UnloadSceneAsync(2);
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
    }

    void PrepareMarket(GameEntity niche, long startCapital)
    {
        // spawn competitors
        for (var i = 0; i < 1; i++)
        {
            var c = Markets.SpawnCompany(niche, Q, Random.Range(2, 5) * startCapital);

            //MarketingUtils.AddClients(c, MarketingUtils.GetClients(c) * Random.Range(1, 1.5f));
            Marketing.AddBrandPower(c, 10);
        }

        // spawn investors
        for (var i = 0; i < 1; i++)
        {
            var fund = Companies.GenerateInvestmentFund(Q, RandomUtils.GenerateInvestmentCompanyName(), 500000);
            Companies.AddFocusNiche(niche.niche.NicheType, fund, Q);
        }
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
