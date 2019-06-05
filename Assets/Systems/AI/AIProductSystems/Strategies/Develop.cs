using Assets.Utils;

public enum ProductActionGoal
{
    IncreaseLoyalty,
    IncreaseIncome,

    DecreaseMaintenance,

    StayInTech
}

public enum ProductCompanyActions
{
    // Product
    UpgradeConcept,

    // IncreaseLoyalty
    UpgradeSegment,
    DecreasePrice,
    BecomeTechLeader,

    // IncreaseIncome
    DisableTargeting,
    UpgradeMonetisation,
    IncreasePrice,

    // DecreaseMaintenance

}

public partial class AIProductSystems : OnDateChange
{
    void Develop(GameEntity company)
    {
        // ---- Team ----
        // +- stop crunches                            cooldown
        // hire someone                             money, mp
        // upgrade team                             mp
        ExpandTeam(company);


        // ---- Product ----
        // * monetisation                             ip, pp
        // n improve segments                         pp, ip
        // n steal ideas                              cooldown
        // increase prices if possible              -cooldown
        IncreasePrices(company);

        // ---- Marketing ----
        // grab clients                             sp, money
        // increase marketing budget if possible


        // ---- Business ----
        // start round                              ????
        // accept investments
        // flip goal                                cooldown
    }

    void ExpandTeam(GameEntity company)
    {
        if (TeamUtils.IsWillNotOverextendTeam(company))
            TeamUtils.HireWorker(company, GetProperWorkerRole(company));
        else
            UpgradeTeam(company);
    }

    void UpgradeTeam(GameEntity company)
    {

    }
}
