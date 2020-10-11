using System;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static int GetTeamSize(GameEntity e)
        {
            return e.team.Workers[WorkerRole.Programmer] + e.team.Managers.Count;
        }

        public static GameEntity GetWorkerByRole(GameEntity company, WorkerRole role, TeamInfo teamInfo, GameContext gameContext)
        {
            var managers = teamInfo.Managers.Select(humanId => Humans.Get(gameContext, humanId));

            var workersWithRole = managers.Where(h => h.worker.WorkerRole == role);

            return workersWithRole.Count() > 0 ? workersWithRole.First() : null;
        }

        public static bool HasRole(GameEntity company, WorkerRole role, TeamInfo teamInfo, GameContext gameContext)
        {
            var managers = teamInfo.Managers.Select(humanId => Humans.Get(gameContext, humanId));

            var workersWithRole = managers.Where(h => h.worker.WorkerRole == role);

            return workersWithRole.Count() > 0;
        }

        public static bool IsNeedsToHireRole(GameEntity company, WorkerRole role, TeamInfo teamInfo, GameContext gameContext)
        {
            var roles = GetRolesForTeam(teamInfo.TeamType);

            if (!roles.Contains(role))
                return false;

            return !HasRole(company, role, teamInfo, gameContext);
        }

        public static void UpdateTeamEfficiency(GameEntity company, GameContext gameContext)
        {
            var featureCap = company.team.Teams.Max(t => GetFeatureRatingCap(company, t, gameContext));

            company.ReplaceTeamEfficiency(new TeamEfficiency
            {
                DevelopmentEfficiency = 100,
                MarketingEfficiency = 100,

                FeatureCap = featureCap, // Products.GetFeatureRatingCap(company, gameContext),
                FeatureGain = GetFeatureRatingGain(company, company.team.Teams[0], gameContext),
            });
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
