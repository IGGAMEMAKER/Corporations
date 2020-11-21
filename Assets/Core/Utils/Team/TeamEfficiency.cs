using System;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static void UpdateTeamEfficiency(GameEntity product, GameContext gameContext)
        {
            // max rating cap 
            var featureCap = product.team.Teams.Max(t => GetFeatureRatingCap(product, t, gameContext));

            // average efficiency

            var competitors = Companies.GetCompetitorsWithSamePositioning(product, gameContext, true);

            var quality = Marketing.GetPositioningQuality(product).Sum();
            var maxQuality = competitors.Select(c => Marketing.GetPositioningQuality(c).Sum()).Max();

            bool isUniqueCompany = competitors.Count() == 1;

            var marketingEfficiency = GetMarketingTeamEffeciency(gameContext, product, isUniqueCompany);

            product.ReplaceTeamEfficiency(new TeamEfficiency
            {
                DevelopmentEfficiency = 100,
                MarketingEfficiency = marketingEfficiency,

                FeatureCap = featureCap, // Products.GetFeatureRatingCap(company, gameContext),
                FeatureGain = GetFeatureRatingGain(product, product.team.Teams[0], gameContext),

                isUniqueCompany = isUniqueCompany,
                Competitiveness = (int)(maxQuality - quality)
            });
        }

        public static int GetMarketingEfficiency(GameEntity company)
        {
            return company.teamEfficiency.Efficiency.MarketingEfficiency;
        }

        public static int GetMarketingTeamEffeciency(GameContext gameContext, GameEntity company, bool isUnique)
        {
            var viableTeams = company.team.Teams
                
                // marketing teams only
                .Where(t => IsUniversalTeam(t.TeamType) || t.TeamType == TeamType.MarketingTeam)
                
                .Select(t => GetTeamEffeciency(company, t) * GetMarketingTeamEffeciency(gameContext, company, t) / 100)
                ;

            if (viableTeams.Count() == 0)
                return 30;

            return (int)viableTeams.Average() * (isUnique ? 2 : 1);
        }

        public static int GetManagerFocusBonus(GameContext gameContext, GameEntity company, TeamInfo teamInfo, ManagerTask managerTask)
        {
            bool hasMainManager = Teams.HasMainManagerInTeam(teamInfo, gameContext, company);

            if (hasMainManager)
            {
                return teamInfo.ManagerTasks.Count(t => t == managerTask);
            }

            return 0;
        }

        public static int GetMarketingTeamEffeciency(GameContext gameContext, GameEntity company, TeamInfo teamInfo)
        {
            var marketingEffeciency = 0;

            marketingEffeciency = Teams.GetEffectiveManagerRating(gameContext, company, WorkerRole.MarketingLead, teamInfo);

            marketingEffeciency *= teamInfo.TeamType == TeamType.MarketingTeam ? 2 : 1;

            marketingEffeciency += Teams.GetManagerFocusBonus(gameContext, company, teamInfo, ManagerTask.ViralSpread) * 10;

            return 50 + marketingEffeciency;
        }

        public static float GetFeatureRatingGain(GameEntity product, TeamInfo team, GameContext gameContext)
        {
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

        public static float GetFeatureRatingCap(GameEntity product, TeamInfo team, GameContext gameContext)
        {
            var productManager = Products.GetWorkerInRole(team, WorkerRole.ProductManager, gameContext);

            var cap = 4f;

            if (team.TeamType == TeamType.DevelopmentTeam)
                cap += 2f;

            if (productManager != null)
            {
                // ... 5f
                var addedCap = 5 * Humans.GetRating(productManager) / 100f;

                return cap + addedCap;
            }

            bool hasMainManager = Teams.HasMainManagerInTeam(team, gameContext, product);
            if (hasMainManager)
            {
                var focus = team.ManagerTasks.Count(t => t == ManagerTask.Polishing);
                cap += focus * 0.4f;
            }

            return Mathf.Clamp(cap, 0, 10);
        }

        public static int GetTeamAverageEffeciency(GameEntity company)
        {
            var average = (int)company.team.Teams.Select(t => GetTeamEffeciency(company, t)).Average();
            return Mathf.Clamp(average, 1, 150);
        }
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
