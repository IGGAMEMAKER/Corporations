using UnityEngine;

public class Constants
{
    public const int DEVELOPMENT_INNOVATION_PENALTY = 7; // 00;
    public const int DEVELOPMENT_CRUNCH_BONUS = 20;
    public const int DEVELOPMENT_PRODUCTION_MARKETER = 15;
    public const int DEVELOPMENT_PRODUCTION_PROGRAMMER = 15;
    public const int DEVELOPMENT_PRODUCTION_MANAGER = 15;

    public const int RISKS_MONETISATION_ADS_REPAYABLE = 35;
    public const int RISKS_MONETISATION_IS_PROFITABLE = 15;
    public const int RISKS_MONETISATION_MAX = RISKS_MONETISATION_ADS_REPAYABLE + RISKS_MONETISATION_IS_PROFITABLE;

    public const int RISKS_DEMAND_MAX = 45;

    public static int INVESTMENT_ROUND_ACTIVE_FOR_DAYS = 60;
}

public class VisualConstants
{
    public const string COLOR_COMPANY_WHERE_I_AM_CEO = "#FFAB04";
    public const string COLOR_COMPANY_SELECTED = "#131679";

    public const string COLOR_LINK = "#FFAB04";
}
