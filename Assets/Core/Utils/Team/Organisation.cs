using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static Bonus<int> GetOrganisationChanges(TeamInfo teamInfo, GameEntity product, GameContext gameContext)
        {
            WorkerRole managerTitle = GetMainManagerRole(teamInfo);

            var rating = GetEffectiveManagerRating(gameContext, product, managerTitle, teamInfo);

            var managerFocus = teamInfo.ManagerTasks.Count(t => t == ManagerTask.Organisation);

            bool NoCeo = rating < 1;

            // count team size: small teams organise faster, big ones: slower

            return new Bonus<int>("Organisation change")
                    .AppendAndHideIfZero("No " + managerTitle, NoCeo ? -30 : 0)
                    .AppendAndHideIfZero(managerTitle + " efforts", rating)
                    .AppendAndHideIfZero("Manager focus on Organisation", NoCeo ? 0 : managerFocus * 10)
                ;
        }
    }
}