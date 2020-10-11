using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static Bonus<int> GetManagerGrowthBonus(GameEntity worker, TeamInfo teamInfo, bool hasTeacherInTeam, GameContext gameContext)
        {
            var rating = Humans.GetRating(worker);

            bool isCurious = worker.humanSkills.Traits.Contains(Trait.Curious);

            var bonus = new Bonus<int>("Growth");
            
            bonus
                //.Append("Base", 25)
                .Append("Rating", (int)Mathf.Pow(100 - rating, 0.95f))
                .AppendAndHideIfZero("Curious", isCurious ? 15 : 0)
                .AppendAndHideIfZero("Works with Teacher", hasTeacherInTeam ? 7 : 0)
                ;

            // market complexity
            // worker current rating (noob - fast growth, senior - slow)
            // trait: curious
            // consultant
            // loyalty change

            bonus.Cap(0, (rating < 100) ? 100 : 0);

            return bonus;
        }
    }
}
