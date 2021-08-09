using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static HumanFF HireManager(GameEntity company, GameContext gameContext, WorkerRole workerRole, int teamId) => HireManager(company, gameContext, Humans.GenerateHuman(gameContext, workerRole), teamId);
        public static HumanFF HireManager(GameEntity company, GameContext gameContext, HumanFF worker, int teamId)
        {
            var role = Humans.GetRole(worker);

            AttachToCompany(company, gameContext, worker, role, teamId);

            company.employee.Managers.Remove(worker.HumanComponent.Id);

            return worker;
        }

        public static void HuntManager(HumanFF worker, GameEntity newCompany, GameContext gameContext, int teamId)
        {
            FireManager(gameContext, worker);

            AttachToCompany(newCompany, gameContext, worker, Humans.GetRole(worker), teamId);
        }

        public static void AttachHumanToTeam(GameEntity company, GameContext gameContext, HumanFF human, WorkerRole role, int teamId)
        {
            var team = company.team.Teams[teamId];

            team.Managers.Add(human);
            team.Roles[human.HumanComponent.Id] = role;

            ReplaceTeam(company, gameContext, company.team);
        }

        public static void AttachToCompany(GameEntity company, GameContext gameContext, HumanFF worker, WorkerRole role, int teamId)
        {
            // add humanId to team
            AttachHumanToTeam(company, gameContext, worker, role, teamId);

            // add companyId to human
            Humans.AttachToCompany(worker, company.company.Id, role);
        }
    }
}
