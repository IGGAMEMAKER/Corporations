public class Balance
{
    public const int PERIOD = 7;

    public const int DEVELOPMENT_PRODUCTION_MARKETER = 10;
    public const int DEVELOPMENT_PRODUCTION_PROGRAMMER = 10;
    public const int DEVELOPMENT_PRODUCTION_MANAGER = 10;
    public const int DEVELOPMENT_PRODUCTION_IDEAS = 100;

    public const int DEVELOPMENT_FOCUS_IDEAS = 40;

    public static int DEVELOPMENT_PRODUCTION_UNIVERSALS = 2;


    public const int RISKS_MONETISATION_ADS_REPAYABLE = 35;
    public const int RISKS_MONETISATION_IS_PROFITABLE = 15;
    public const int RISKS_MONETISATION_MAX = RISKS_MONETISATION_IS_PROFITABLE;

    public const int RISKS_DEMAND_MAX = 45;

    public const int RISKS_NICHE_COMPETITIVENESS_MAX = 33;

    public const int INVESTMENT_ROUND_ACTIVE_FOR_DAYS = 60;

    public const long CORPORATION_REQUIREMENTS_COMPANY_COST = 100000000;

    public const long IPO_REQUIREMENTS_COMPANY_COST = CORPORATION_REQUIREMENTS_COMPANY_COST * 5;
    public const long IPO_REQUIREMENTS_COMPANY_PROFIT = 10000000;

    public const long INVESTMENT_GOAL_GROWTH_REQUIREMENT_COMPANY_COST = 20;
    public const long INVESTMENT_GOAL_GROWTH_REQUIREMENT_PROFIT_GROWTH = 5;

    // settings
    public const string MENU_SELECTED_COMPANY = "SelectedCompany";
    public const string MENU_SELECTED_INDUSTRY = "SelectedIndustry";
    public const string MENU_SELECTED_NICHE = "SelectedNiche";
    public const string MENU_SELECTED_HUMAN = "SelectedHuman";
    public const string MENU_SELECTED_INVESTOR = "SelectedInvestor";
    //public static float GAMEPLAY_OFFSET_Y = -110f;
    public const float GAMEPLAY_OFFSET_Y = -60f;


    public const int SALARIES_MANAGER = 2000;
    public const int SALARIES_PROGRAMMER = 2000;
    public const int SALARIES_MARKETER = 2000;
    public const int SALARIES_UNIVERSAL = 0;
    public const int SALARIES_CEO = 0;
    public const int SALARIES_FOUNDER = 0;
    public const int SALARIES_DIRECTOR = 3500;
    public const int SALARIES_PRODUCT_PROJECT_MANAGER = 2500;


    public const int COOLDOWN_COMPANY_GOAL = 365;
    public const int COOLDOWN_BRANDING = 90;
    public const int COOLDOWN_TEST_CAMPAIGN = 15;

    public static int COOLDOWN_CONCEPT = 15;
    public static int RELEASE_BRAND_POWER_GAIN = 30;
    public static int INNOVATION_BRAND_POWER_GAIN = 10;
    public static int REVOLUTION_BRAND_POWER_GAIN = 25;
    public static float BRAND_CAMPAIGN_BRAND_POWER_GAIN = 1f;

    public const int CORPORATE_CULTURE_CHANGES_DURATION = 30;
    public const int CORPORATE_CULTURE_LEVEL_MAX = 10;
    public const int CORPORATE_CULTURE_LEVEL_MIN = 1;

    //public static int START_YEAR = 1991;
    public const int START_YEAR = 2000; // 2000
    public const long COMPANY_DESIRE_TO_SELL_NO = 0;
    public const long COMPANY_DESIRE_TO_SELL_YES = 1;

    public const float CLIENT_GAIN_MODIFIER_MIN = 1f;
    public const float CLIENT_GAIN_MODIFIER_MAX = 1.5f;

    public const int FINANCING_ITERATION_SPEED_PER_LEVEL = 10;


    public const int CULTURE_ITERATION_SPEED_PER_LEVEL = 5;

    public const int IDEA_PER_EXPERTISE = 1000;

    public const int AMOUNT_OF_INVESTORS_ON_STARTING_NICHE = 4;
}

public class Colors
{
    const string COLOR_BACKGROUND = "#2B2E34";
    public const string COLOR_GOLD = "#FFAB04";
    public const string COLOR_WHITE = "#FFFFFF";
    public const string COLOR_VIOLET = "#524FDE";

    public const string COLOR_YOU = COLOR_GOLD;
    public const string COLOR_CONTROL = COLOR_GOLD;
    public const string COLOR_CONTROL_NO = COLOR_WHITE;
    public const string COLOR_COMPANY_WHERE_I_AM_CEO = COLOR_GOLD;
    public const string COLOR_COMPANY_WHERE_I_AM_NOT_CEO = COLOR_WHITE;
    public const string COLOR_COMPANY_SELECTED = "#131679";

    public const string COLOR_PARTNERSHIP = COLOR_VIOLET;

    public const string COLOR_PANEL_BASE = COLOR_WHITE;
    public const string COLOR_PANEL_SELECTED = COLOR_COMPANY_SELECTED;

    public const string COLOR_POSITIVE = "#00FF00";
    public const string COLOR_NEGATIVE = "#FF0000";
    public const string COLOR_NEUTRAL = COLOR_WHITE;
    public const string COLOR_BEST = COLOR_GOLD; // "#FFD100"; // "#FFA0FF";


    public const string COLOR_MARKET_ATTITUDE_HAS_COMPANY = COLOR_GOLD; // "#FFD100"; // "#FFA0FF";
    public const string COLOR_MARKET_ATTITUDE_FOCUS_ONLY = "#524FDE"; // "#FFD100"; // "#FFA0FF";
    public const string COLOR_MARKET_ATTITUDE_NOT_INTERESTED = COLOR_CONTROL_NO; // "#FFD100"; // "#FFA0FF";


    public const string COLOR_LINK = "magenta";
}
