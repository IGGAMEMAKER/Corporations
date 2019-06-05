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
    void Develop(GameEntity product)
    {
        // ---- Team ----
        // +- stop crunches                            cooldown
        // hire someone                             money, mp
        // upgrade team                             mp


        // ---- Product ----
        // * monetisation                             ip, pp
        // n improve segments                         pp, ip
        // n steal ideas                              cooldown
        // increase prices if possible              -cooldown

        // ---- Marketing ----
        // grab clients                             sp, money
        // increase marketing budget if possible


        // ---- Business ----
        // start round                              ????
        // accept investments
        // flip goal                                cooldown
    }
}
