using System;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static void UpdateTeamEfficiency(GameEntity product, GameContext gameContext)
        {
            // competition
            var competitors = Companies.GetCompetitorsWithSamePositioning(product, gameContext, true);

            var quality = Marketing.GetPositioningQuality(product).Sum();
            var maxQuality = competitors.Select(c => Marketing.GetPositioningQuality(c).Sum()).Max();

            bool isUniqueCompany = competitors.Count() == 1;
            // -------------------------------------------------

            product.ReplaceTeamEfficiency(new TeamEfficiency
            {
                DevelopmentEfficiency = GetDevelopmentTeamEfficiency(gameContext, product),
                MarketingEfficiency = GetMarketingTeamEffeciency(gameContext, product, isUniqueCompany),

                FeatureCap = GetMaxFeatureRatingCap(product, gameContext).Sum(), // Products.GetFeatureRatingCap(company, gameContext),
                FeatureGain = GetFeatureRatingGain(product, product.team.Teams[0], gameContext),

                isUniqueCompany = isUniqueCompany,
                Competitiveness = (int)(maxQuality - quality)
            });
        }

        public static int GetDevelopmentEfficiency(GameEntity company)
        {
            return company.teamEfficiency.Efficiency.DevelopmentEfficiency;
        }

        public static int GetMarketingEfficiency(GameEntity company)
        {
            return company.teamEfficiency.Efficiency.MarketingEfficiency;
        }

        // -------------- DEV -----------------------
        public static int GetDevelopmentTeamEfficiency(GameContext gameContext, GameEntity product, TeamInfo teamInfo)
        {
            var efficiency = 0;

            efficiency = GetEffectiveManagerRating(gameContext, product, WorkerRole.TeamLead, teamInfo);

            efficiency += teamInfo.TeamType == TeamType.DevelopmentTeam ? 30 : 0;

            //efficiency += Teams.GetManagerFocusBonus(gameContext, product, teamInfo, ManagerTask.ViralSpread) * 10;

            return (10 + efficiency) * GetTeamEffeciency(product, teamInfo);
        }

        private static int GetDevelopmentTeamEfficiency(GameContext gameContext, GameEntity product)
        {
            var viableTeams = product.team.Teams

            // dev teams only
            .Where(t => IsUniversalTeam(t.TeamType) || t.TeamType == TeamType.DevelopmentTeam)

            .Select(t => GetDevelopmentTeamEfficiency(gameContext, product, t) / 100)
            ;

            if (viableTeams.Count() == 0)
                return 50;

            return (int)viableTeams.Average();
        }

        // -------------- MARKETING --------------------------------
        public static int GetMarketingTeamEffeciency(GameContext gameContext, GameEntity company, bool isUnique)
        {
            var viableTeams = company.team.Teams
                
                // marketing teams only
                .Where(t => IsUniversalTeam(t.TeamType) || t.TeamType == TeamType.MarketingTeam)
                
                .Select(t => GetMarketingTeamEffeciency(gameContext, company, t) / 100)
                ;

            if (viableTeams.Count() == 0)
                return 30;

            return (int)viableTeams.Average() * (isUnique ? 2 : 1);
        }


        public static int GetMarketingTeamEffeciency(GameContext gameContext, GameEntity product, TeamInfo teamInfo)
        {
            var marketingEffeciency = 0;

            marketingEffeciency = Teams.GetEffectiveManagerRating(gameContext, product, WorkerRole.MarketingLead, teamInfo);

            marketingEffeciency *= teamInfo.TeamType == TeamType.MarketingTeam ? 2 : 1;

            //marketingEffeciency += Teams.GetManagerFocusBonus(gameContext, company, teamInfo, ManagerTask.ViralSpread) * 10;

            return (50 + marketingEffeciency) * GetTeamEffeciency(product, teamInfo);
        }


        // --------------------------------

        public static int GetManagerFocusBonus(GameContext gameContext, GameEntity company, TeamInfo teamInfo, ManagerTask managerTask)
        {
            bool hasMainManager = Teams.HasMainManagerInTeam(teamInfo, gameContext, company);

            if (hasMainManager)
            {
                return teamInfo.ManagerTasks.Count(t => t == managerTask);
            }

            return 0;
        }


        // --------------------- FEATURES --------------------------
        public static float GetFeatureRatingGain(GameEntity product, TeamInfo team, GameContext gameContext)
        {
            return 1f;
            var speed = 0.2f;

            // 0.4f ... 1f
            var gain = Teams.GetEffectiveManagerRating(gameContext, product, WorkerRole.ProductManager, team) / 100f;
            speed += gain;

            bool isDevTeam = team.TeamType == TeamType.DevelopmentTeam;
            if (isDevTeam)
            {
                speed += 0.3f;
                speed += gain;
            }

            return speed;
        }

        public static Bonus<float> GetMaxFeatureRatingCap(GameEntity product, GameContext gameContext)
        {
            return product.team.Teams
                .Select(t => GetFeatureRatingCap(product, t, gameContext))
                .OrderByDescending(b => b.Sum())
                .First();
        }

        public static Bonus<float> GetFeatureRatingCap(GameEntity product, TeamInfo team, GameContext gameContext)
        {
            var productManager = Products.GetWorkerInRole(team, WorkerRole.ProductManager, gameContext);

            var bonus = new Bonus<float>("Max feature lvl");

            bonus.Append("Base", 1f);
            bonus.AppendAndHideIfZero("Development Team", team.TeamType == TeamType.DevelopmentTeam ? 1f : 0);

            if (productManager != null)
            {
                // ... 5f
                var addedCap = 3 * Humans.GetRating(productManager) / 100f;

                bonus.AppendAndHideIfZero("Product Manager", addedCap);
            }

            var culture = Companies.GetOwnCulture(Companies.GetManagingCompanyOf(product, gameContext));

            var cultureBonus = (10 - culture[CorporatePolicy.DoOrDelegate]) * 0.2f;
            var cultureBonus2 = (culture[CorporatePolicy.DecisionsManagerOrTeam]) * 0.4f;

            bonus.Append("Corporate culture Do or Delegate", cultureBonus);
            bonus.Append("Corporate culture Manager or Team", cultureBonus2);


            //bool hasMainManager = Teams.HasMainManagerInTeam(team, gameContext, product);
            //if (hasMainManager)
            //{
            //    var focus = team.ManagerTasks.Count(t => t == ManagerTask.Polishing);
            //    cap += focus * 0.4f;
            //}

            bonus.Cap(0, 10);

            return bonus;

            //return Mathf.Clamp(cap, 0, 10);
        }

        // ------------------------------------------------------------------

        public static int GetTeamEffeciency(GameEntity company, TeamInfo teamInfo)
        {
            var maxWorkers = 8;

            var organisationBonus = (int)teamInfo.Organisation;

            // 0... 130
            return teamInfo.Workers * (100 + 30 * organisationBonus / 100) / maxWorkers + 1;
        }

        public static int GetWorkerEffeciency(GameEntity worker, GameEntity company)
        {
            if (worker == null)
                return 0;

            var expertise = 0;

            if (company.hasProduct && worker.humanSkills.Expertise.ContainsKey(company.product.Niche))
                expertise = worker.humanSkills.Expertise[company.product.Niche];

            var adaptability = worker.humanCompanyRelationship.Adapted == 100 ? 100 : 30;

            return (70 * adaptability + 30 * expertise) / 100;
        }
    }
}
