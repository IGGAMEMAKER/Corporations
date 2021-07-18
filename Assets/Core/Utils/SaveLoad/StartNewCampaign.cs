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

            var group = PreparePlayerCompany(niche, startCapital, text, gameContext);
            PrepareMarket(niche, startCapital, gameContext);

            var flagship = Companies.CreateProductAndAttachItToGroup(gameContext, NicheType, group);
            Companies.TurnProductToPlayerFlagship(flagship, gameContext, NicheType, group);

            LoadGameScene();
        }

        public static void LoadGameScene()
        {
            //SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

            if (SceneManager.GetSceneByBuildIndex(2).isLoaded)
                SceneManager.UnloadSceneAsync(2);

            SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        }

        internal static GameEntity PreparePlayerCompany(GameEntity niche, long startCapital, string text, GameContext gameContext)
        {
            var company = Companies.GenerateCompanyGroup(gameContext, text);

            company.ReplaceCorporateCulture(Companies.GetPlayerCorporateCulture());


            Companies.SetResources(company, startCapital, "start capital");

            Companies.PlayAs(company, gameContext);

            Investments.SetGrowthStyle(company, CompanyGrowthStyle.None);
            Investments.SetVotingStyle(company, VotingStyle.None);
            Investments.SetExitStrategy(company, InvestorInterest.None);

            Companies.AutoFillShareholders(gameContext, company, true);

            return company;
        }



        internal static void PrepareMarket(GameEntity niche, long startCapital, GameContext gameContext)
        {
            var segments = Marketing.GetAudienceInfos();

            // spawn competitors
            for (var i = 0; i < 5; i++)
            {
                var funds = Random.Range(20, 50) * startCapital;
                var c = Markets.SpawnCompany(niche, gameContext, funds);

                var features = Products.GetAllFeaturesForProduct();
                var teams = Random.Range(3, 9);

                for (var j = 0; j < teams; j++)
                {
                    Teams.AddTeam(c, gameContext, TeamType.CrossfunctionalTeam);
                }

                foreach (var f in features)
                {
                    if (f.FeatureBonus.isRetentionFeature)
                    {
                        Products.ForceUpgradeFeature(c, f.Name, Random.Range(2f, 5f), gameContext);
                    }
                }

                var clients = 50_000d * Mathf.Pow(10, Random.Range(0.87f, 2.9f)) * (i + 1);

                //var positioning = c.productPositioning.Positioning;
                foreach (var s in segments)
                {
                    if (s.ID == Marketing.GetCoreAudienceId(c))
                    {
                        var audience = System.Convert.ToInt64(clients * Random.Range(0.1f, 0.5f));
                        Marketing.AddClients(c, audience, s.ID);
                    }
                }
            }

            // spawn investors
            for (var i = 0; i < C.AMOUNT_OF_INVESTORS_ON_STARTING_NICHE; i++)
            {
                var fund = Companies.GenerateInvestmentFund(gameContext, RandomUtils.GenerateInvestmentCompanyName(), 500000);
                Companies.AddFocusNiche(fund, niche.niche.NicheType, gameContext);
            }
        }
    }
}
