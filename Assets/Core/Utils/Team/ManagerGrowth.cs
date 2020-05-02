using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Core
{
    public static partial class Teams
    {
        public static Bonus<int> GetManagerGrowthBonus(GameEntity worker, GameContext gameContext)
        {
            var loyaltyChange = GetLoyaltyChangeForManager(worker, gameContext);
            var rating = Humans.GetRating(worker);
            var ratingBased = 100 - rating; // 70...0

            var bonus = new Bonus<int>("Growth");

            bonus
                .Append("Base", 25)
                .Append("Loyalty change", loyaltyChange * 2)
                .Append("Rating", (int)Mathf.Sqrt(ratingBased))
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
