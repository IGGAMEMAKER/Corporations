using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace Assets.Core
{
    public partial class State
    {
        // Start new Campaign
        public static void StartNewCampaign(GameContext gameContext, NicheType NicheType, string text)
        {
            var company = Companies.GenerateCompanyGroup(gameContext, text);

            var half = Balance.CORPORATE_CULTURE_LEVEL_MAX / 2;

            company.ReplaceCorporateCulture(new Dictionary<CorporatePolicy, int>
            {
                [CorporatePolicy.BuyOrCreate] = half,
                [CorporatePolicy.FocusingOrSpread] = 1,
                [CorporatePolicy.LeaderOrTeam] = 1,
                [CorporatePolicy.InnovationOrStability] = half,
                [CorporatePolicy.SalariesLowOrHigh] = half,
                [CorporatePolicy.CompetitionOrSupport] = half
            });

            var startCapital = Markets.GetStartCapital(NicheType, gameContext);

            Companies.SetResources(company, new TeamResource(startCapital));

            var niche = Markets.Get(gameContext, NicheType);
            //niche.AddResearch(1);

            Companies.PlayAs(company, gameContext);
            Companies.AutoFillShareholders(gameContext, company, true);

            PrepareMarket(niche, startCapital, gameContext);


            ScreenUtils.Navigate(gameContext, ScreenMode.NicheScreen, Balance.MENU_SELECTED_NICHE, NicheType);


            SceneManager.UnloadSceneAsync(2);
            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        }

        internal static void PrepareMarket(GameEntity niche, long startCapital, GameContext gameContext)
        {
            // spawn competitors
            for (var i = 0; i < 1; i++)
            {
                var funds = UnityEngine.Random.Range(2, 5) * startCapital;
                var c = Markets.SpawnCompany(niche, gameContext, funds);

                var newLevel = UnityEngine.Random.Range(5, 15);

                Marketing.AddClients(c, Marketing.GetClientFlow(gameContext, c.product.Niche) * newLevel);
                Marketing.AddBrandPower(c, newLevel * 3);
                Products.ForceUpgrade(c, gameContext, newLevel);
            }

            // spawn investors
            for (var i = 0; i < Balance.AMOUNT_OF_INVESTORS_ON_STARTING_NICHE; i++)
            {
                var fund = Companies.GenerateInvestmentFund(gameContext, RandomUtils.GenerateInvestmentCompanyName(), 500000);
                Companies.AddFocusNiche(niche.niche.NicheType, fund, gameContext);
            }
        }
    }
}
