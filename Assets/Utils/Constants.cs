using UnityEngine;

public class Constants
{
    public const int DEVELOPMENT_INNOVATION_PENALTY = 700; // 00;
    public const int DEVELOPMENT_CRUNCH_BONUS = 20;

    public const int DEVELOPMENT_PRODUCTION_MARKETER = 10;
    public const int DEVELOPMENT_PRODUCTION_PROGRAMMER = 10;
    public const int DEVELOPMENT_PRODUCTION_MANAGER = 10;
    public const int DEVELOPMENT_PRODUCTION_IDEAS = 10;

    public const int DEVELOPMENT_FOCUS_IDEAS = 40;

    public static int DEVELOPMENT_PRODUCTION_UNIVERSALS = 2;


    public const int RISKS_MONETISATION_ADS_REPAYABLE = 35;
    public const int RISKS_MONETISATION_IS_PROFITABLE = 15;
    public const int RISKS_MONETISATION_MAX = RISKS_MONETISATION_IS_PROFITABLE;

    public const int RISKS_DEMAND_MAX = 45;

    public const int RISKS_NICHE_COMPETITIVENESS_MAX = 33;

    public static int INVESTMENT_ROUND_ACTIVE_FOR_DAYS = 60;

    internal static long IPO_REQUIREMENTS_COMPANY_COST = 500000000;
    internal static long IPO_REQUIREMENTS_COMPANY_PROFIT = 10000000;

    public const long INVESTMENT_GOAL_GROWTH_REQUIREMENT_COMPANY_COST = 20;
    public const long INVESTMENT_GOAL_GROWTH_REQUIREMENT_PROFIT_GROWTH = 5;

    public const string MENU_SELECTED_COMPANY = "SelectedCompany";
    public const string MENU_SELECTED_INDUSTRY = "SelectedIndustry";
    public const string MENU_SELECTED_NICHE = "SelectedNiche";
    public const string MENU_SELECTED_HUMAN = "SelectedHuman";

    public static int SALARIES_MANAGER = 2000;
    public static int SALARIES_PROGRAMMER = 2000;
    public static int SALARIES_MARKETER = 2000;
    public static int SALARIES_UNIVERSAL = 2000;
    public static int SALARIES_CEO = 0;
    public static int SALARIES_FOUNDER = 0;
    public static int SALARIES_DIRECTOR = 3500;
    public static int SALARIES_PRODUCT_PROJECT_MANAGER = 2500;

    public static long COMPANY_SCORING_BANKRUPTCY = 10000;
    public static long COMPANY_SCORING_LOYALTY = 5000;
    public static long COMPANY_SCORING_COMPANY_GOAL = 3000;
}

public class VisualConstants
{
    const string COLOR_BACKGROUND = "#2B2E34";
    public const string COLOR_GOLD = "#FFAB04";

    public const string COLOR_COMPANY_WHERE_I_AM_CEO = COLOR_GOLD;
    public const string COLOR_COMPANY_WHERE_I_AM_NOT_CEO = "#FFFFFF";
    public const string COLOR_COMPANY_SELECTED = "#131679";

    public const string COLOR_POSITIVE = "#00FF00";
    public const string COLOR_NEGATIVE = "#FF0000";
    public const string COLOR_NEUTRAL = "#FFFFFF";
    public const string COLOR_BEST = COLOR_GOLD; // "#FFD100"; // "#FFA0FF";


    public const string COLOR_LINK = "#FFAB04";
}
