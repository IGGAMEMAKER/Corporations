using Assets.Core;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class HireNewManagersSystem : OnPeriodChange
{
    public HireNewManagersSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        var playerFlagship = Companies.GetPlayerFlagship(gameContext);

        foreach (var e in Companies.GetProductCompanies(gameContext))
        {
            if (e.company.Id != playerFlagship.company.Id)
                HireManagers(e, playerFlagship);
        }
    }

    void HireManagers(GameEntity company, GameEntity playerFlagship)
    {
        // recruit workers from competing companies
        if (Companies.IsInPlayerSphereOfInterest(company, gameContext))
        {
            // try to recruit player workers
            var aggressiveness = 10 - company.corporateCulture.Culture[CorporatePolicy.CompetitionOrSupport];

            var wantsToRecruit = Random.Range(0, 1000) < 10 * aggressiveness;

            int teamId = Random.Range(0, playerFlagship.team.Teams.Count);
            int managerId = Random.Range(0, playerFlagship.team.Teams[teamId].Managers.Count);

            if (teamId == 0 && managerId == 0)
                return;

            if (wantsToRecruit)
            {
                var humanId = playerFlagship.team.Teams[teamId].Managers[managerId];
                var worker = Humans.Get(gameContext, humanId);

                var rating = Humans.GetRating(worker);
                var neediness = Mathf.Clamp(Mathf.Pow(rating, 0.3f), 1f, 3f);

                var salary = (long)(Humans.GetSalary(worker) * neediness);
                var jobOffer = new JobOffer(salary);

                if (Economy.IsCanMaintain(company, gameContext, salary))
                {
                    Teams.SendJobOffer(worker, jobOffer, company, gameContext);

                    NotificationUtils.AddNotification(gameContext, new NotificationMessageManagerRecruiting(humanId, company.company.Id, false));
                }
            }
        }

        // hire employees
        foreach (var t in company.team.Teams)
        {
            var allRoles = Teams.GetRolesForTeam(t.TeamType);
            var currentRoles = t.Roles.Values.ToArray();

            var necessaryRoles = allRoles.Where(r => !currentRoles.Contains(r));

            if (necessaryRoles.Count() > 0)
            {
                var rating = Teams.GetTeamAverageStrength(company, gameContext) + Random.Range(-2, 3);
                var salary = Teams.GetSalaryPerRating(rating);

                if (Economy.IsCanMaintain(company, gameContext, salary))
                {
                    var human = Teams.HireManager(company, gameContext, necessaryRoles.First(), t.ID);

                    Teams.SetJobOffer(human, company, new JobOffer(salary), t.ID, gameContext);
                }
                else
                {
                    break;
                }
            }
        }


        // move duplicate workers to other teams or replace weaker ones




        //var roles = Teams.GetRolesTheoreticallyPossibleForThisCompanyType(company);
        //var haveRoles = company.team.Managers.Values;

        //var needRoles = roles.Where(r => !haveRoles.Contains(r));

        //foreach (var r in needRoles)
        //{
        //    if (Economy.IsCanMaintain(company, gameContext, managerCost))
        //        Teams.HireManager(company, gameContext, r);
        //}
    }
}
