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
            var managers = teamInfo.Managers.Select(humanId => Humans.GetHuman(gameContext, humanId));

            var workersWithRole = managers.Where(h => h.worker.WorkerRole == role);

            return workersWithRole.Count() > 0 ? workersWithRole.First() : null;
        }

        public static bool HasRole(GameEntity company, WorkerRole role, TeamInfo teamInfo, GameContext gameContext)
        {
            var managers = teamInfo.Managers.Select(humanId => Humans.GetHuman(gameContext, humanId));

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
            return teamInfo.Workers * (100 + 30 * organisationBonus / 100) / maxWorkers;
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
