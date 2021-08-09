using System;
using System.Collections.Generic;
using System.Linq;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static Bonus<float> GetDirectManagementCostOfTeam(TeamInfo team, GameEntity company, GameContext gameContext)
        {
            var bonus = new Bonus<float>("Cost of " + team.Name);

            var managers = GetPeople(team, gameContext);
            var mainRole = GetMainManagerRole(team);

            // 50...90
            foreach (var m in managers)
            {
                var rating = Humans.GetRating(m);

                // Lead gain
                if (Humans.GetRole(m) == mainRole)
                {
                    bonus.Append($"{mainRole}  <b>{rating}lvl</b>", rating / 10f);
                    continue;
                }

                bonus.Append($"{m.HumanComponent.Name} {m.HumanComponent.Surname} <b>{rating}lvl</b>", (rating - 150) / 100f);
            }

            var processes = GetPolicyValueModified(company, CorporatePolicy.PeopleOrProcesses, 1f, 0.5f, 0.25f);

            bonus.MultiplyAndHideIfOne("From corporate policies", processes);

            team.isManagedBadly = bonus.Sum() < 0;

            return bonus;
        }


        // TODO rewrite for more performance
        public static IEnumerable<HumanFF> GetPeople(TeamInfo team, GameContext gameContext)
        {
            return team.Managers;
            //return team.Managers.Select(humanId => Humans.Get(gameContext, humanId));
        }
    }
}
