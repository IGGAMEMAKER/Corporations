using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Classes
{
    public class Commands
    {
        public const string AD_CAMPAIGN_START = "AD_CAMPAIGN_START";
        public const string AD_CAMPAIGN_PREPARE = "AD_CAMPAIGN_PREPARE";
        public const string FEATURE_EXPLORE = "FEATURE_EXPLORE";
        public const string FEATURE_UPGRADE = "FEATURE_UPGRADE";

        public const string TEAM_WORKERS_HIRE = "TEAM_WORKERS_HIRE";
        public const string TEAM_WORKERS_FIRE = "TEAM_WORKERS_FIRE";
        public const string TEAM_SALARIES_INCREASE = "TEAM_SALARIES_INCREASE";
        public const string TEAM_SALARIES_DECREASE = "TEAM_SALARIES_DECREASE";

        public const string COMPANIES_IDEAS_STEAL = "COMPANIES_IDEAS_STEAL";

        public const string SHARES_SELL = "SHARES_SELL";
        public const string SHARES_BUY = "SHARES_BUY";
    }
}
