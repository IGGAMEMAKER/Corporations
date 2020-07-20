using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Core
{
    public partial class State
    {
        // Start new Campaign
        public static void StartNewCampaign(GameContext gameContext, NicheType NicheType, string text)
        {
            var startCapital = Markets.GetStartCapital(NicheType, gameContext);
            var niche = Markets.Get(gameContext, NicheType);

            PreparePlayerCompany(niche, startCapital, text, gameContext);
            PrepareMarket(niche, startCapital, gameContext);

            ScreenUtils.Navigate(gameContext, ScreenMode.NicheScreen, C.MENU_SELECTED_NICHE, NicheType);
            //ScreenUtils.Navigate(gameContext, ScreenMode.HoldingScreen, C.MENU_SELECTED_NICHE, NicheType);

            LoadGameScene();
        }

        public static void LoadGameScene()
        {
            //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

            if (SceneManager.GetSceneByBuildIndex(2).isLoaded)
                SceneManager.UnloadSceneAsync(2);

            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        }

        internal static void PreparePlayerCompany(GameEntity niche, long startCapital, string text, GameContext gameContext)
        {
            var company = Companies.GenerateCompanyGroup(gameContext, text);

            var half = C.CORPORATE_CULTURE_LEVEL_MAX / 2;

            company.ReplaceCorporateCulture(new Dictionary<CorporatePolicy, int>
            {
                [CorporatePolicy.CompetitionOrSupport] = half,
                [CorporatePolicy.SalariesLowOrHigh] = half,
                [CorporatePolicy.Delegate] = 1,
                [CorporatePolicy.FocusingOrSpread] = 1, // doesn't matter
                [CorporatePolicy.Make] = 1,
                [CorporatePolicy.Sell] = 1,
            });


            Companies.SetResources(company, new TeamResource(startCapital));

            //niche.AddResearch(1);

            Companies.PlayAs(company, gameContext);
            Companies.AutoFillShareholders(gameContext, company, true);


            ///
            return;
            //int productId = Companies.CreateProductAndAttachItToGroup(gameContext, NicheType.ECom_Exchanging, company);
            //var Flagship = Companies.Get(gameContext, productId);

            //Debug.Log("AUTOSTARTING TEST CAMPAIGN: " + Flagship?.company.Name);

            //Marketing.AddClients(Flagship, 500);
            //Products.UpgradeProductLevel(Flagship, gameContext);

            //Teams.AddTeam(Flagship, TeamType.CrossfunctionalTeam);
            //Marketing.ReleaseApp(gameContext, Flagship);
        }

        internal static void PrepareMarket(GameEntity niche, long startCapital, GameContext gameContext)
        {
            // spawn competitors
            for (var i = 0; i < 1; i++)
            {
                var funds = UnityEngine.Random.Range(2, 5) * startCapital;
                var c = Markets.SpawnCompany(niche, gameContext, funds);

                //var newLevel = UnityEngine.Random.Range(4, 8);
                var newLevel = UnityEngine.Random.Range(1, 2);

                //Marketing.AddClients(c, Marketing.GetClientFlow(gameContext, c.product.Niche) * newLevel);
                Marketing.AddBrandPower(c, newLevel * 3);
                Products.ForceUpgrade(c, gameContext, newLevel);
            }

            // spawn investors
            for (var i = 0; i < C.AMOUNT_OF_INVESTORS_ON_STARTING_NICHE; i++)
            {
                var fund = Companies.GenerateInvestmentFund(gameContext, RandomUtils.GenerateInvestmentCompanyName(), 500000);
                Companies.AddFocusNiche(niche.niche.NicheType, fund, gameContext);
            }
        }
    }
}
