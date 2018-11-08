using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Classes
{
    public class Balance
    {
        public static int advertClientsRangeMin = 100; // percent
        internal static float advertClientsRangeMax = 175;
        internal static int advertEffeciencyRangeMin = 100;
        internal static int advertEffeciencyRangeMax = 100;
        internal static int maxAmountOfTraits = 3;

        // Team morale bonuses
        public static int MORALE_BONUS_BASE = 30;
        public static int MORALE_BONUS_IS_TEAM = 10;
        public static int MORALE_BONUS_IS_PROFITABLE = 10;
        public static int MORALE_BONUS_IS_PRESTIGEOUS_COMPANY = 5;
        public static int MORALE_BONUS_IS_INNOVATIVE = 10;
        public static int MORALE_PENALTY_COST_PER_WORKER = -3;

        // personal bonuses
        public static int MORALE_PERSONAL_BASE = -50;

        public static int SKILL_MAX_LEVEL = 10;

        public static int BASE_INVESTOR_ID = 1;
    }
}
