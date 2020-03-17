using Entitas;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Core
{
    public static partial class ScreenUtils
    {
        public static GameEntity GetMenu(GameContext gameContext)
        {
            return gameContext.GetEntities(GameMatcher.Menu)[0];
        }

        public static Dictionary<string, object> GetScreenData(GameContext context)
        {
            return GetMenu(context).menu.Data;
        }

        public static ScreenMode GetScreenMode(GameContext context)
        {
            return GetMenu(context).menu.ScreenMode;
        }

        public static GameEntity GetSelectedCompany(GameContext gameContext)
        {
            int companyId = (int)GetScreenData(gameContext)[Balance.MENU_SELECTED_COMPANY];

            return Companies.Get(gameContext, companyId);
        }

        public static GameEntity GetSelectedInvestor(GameContext gameContext)
        {
            int id = (int)GetScreenData(gameContext)[Balance.MENU_SELECTED_INVESTOR];

            return Investments.GetInvestorById(gameContext, id);
        }

        public static IndustryType GetSelectedIndustry(GameContext gameContext)
        {
            return (IndustryType)GetScreenData(gameContext)[Balance.MENU_SELECTED_INDUSTRY];
        }

        public static NicheType GetSelectedNiche(GameContext gameContext)
        {
            return (NicheType)GetScreenData(gameContext)[Balance.MENU_SELECTED_NICHE];
        }

        public static GameEntity GetSelectedHuman(GameContext gameContext)
        {
            var humanId = (int) GetScreenData(gameContext)[Balance.MENU_SELECTED_HUMAN];

            return Humans.GetHuman(gameContext, humanId);
        }


        public static void SetSelectedHuman(GameContext gameContext, int humanId)
        {
            var menu = GetMenu(gameContext);

            var data = menu.menu.Data;

            if ((int)data[Balance.MENU_SELECTED_HUMAN] == humanId)
                return;

            data[Balance.MENU_SELECTED_HUMAN] = humanId;

            ReplaceMenu(gameContext, menu.menu.ScreenMode, data);
        }


        public static void SetSelectedCompany(GameContext gameContext, int companyId)
        {
            var menu = GetMenu(gameContext);

            var data = menu.menu.Data;

            if ((int)data[Balance.MENU_SELECTED_COMPANY] == companyId)
                return;

            data[Balance.MENU_SELECTED_COMPANY] = companyId;

            ReplaceMenu(gameContext, menu.menu.ScreenMode, data);
        }


        public static void SetSelectedNiche(GameContext gameContext, NicheType nicheType)
        {
            var menu = GetMenu(gameContext);

            var data = menu.menu.Data;

            if ((NicheType)data[Balance.MENU_SELECTED_NICHE] == nicheType)
                return;

            data[Balance.MENU_SELECTED_NICHE] = nicheType;

            ReplaceMenu(gameContext, menu.menu.ScreenMode, data);
        }

        public static void ReplaceMenu(GameContext gameContext, ScreenMode screenMode, Dictionary<string, object> data)
        {
            var menu = GetMenu(gameContext);

            menu.ReplaceMenu(screenMode, data);
        }

        public static void TriggerScreenUpdate(GameContext gameContext)
        {
            var menu = GetMenu(gameContext);

            menu.ReplaceMenu(menu.menu.ScreenMode, menu.menu.Data);
        }

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

            var niche = Markets.GetNiche(gameContext, NicheType);
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
                var c = Markets.SpawnCompany(niche, gameContext, Random.Range(2, 5) * startCapital);

                //MarketingUtils.AddClients(c, MarketingUtils.GetClients(c) * Random.Range(1, 1.5f));
                Marketing.AddBrandPower(c, 10);
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
