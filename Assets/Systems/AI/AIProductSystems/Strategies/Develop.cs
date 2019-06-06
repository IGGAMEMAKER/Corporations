using Assets.Utils;


public interface IAIProductCompany
{
    void Crunch(GameEntity company);

    void IncreasePrices(GameEntity company);
    void DecreasePrices(GameEntity company);

    void ExpandTeam(GameEntity company);
    void ShrinkTeam(GameEntity company);
}

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
    void CompleteCompanyGoal(GameEntity company)
    {
        switch (company.companyGoal.InvestorGoal)
        {
            case InvestorGoal.BecomeMarketFit: BecomeMarketFit(company); break;
            case InvestorGoal.BecomeProfitable: break;
            case InvestorGoal.GrowCompanyCost: break;
            case InvestorGoal.IPO: break;
        }


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
        {
            TeamUtils.HireWorker(company, GetProperWorkerRole(company));
        }
        else
        {
            UpgradeTeam(company);
        }
    }

    void UpgradeTeam(GameEntity company)
    {
        var status = company.team.TeamStatus;

        TeamUtils.Promote(company);

        if (status == TeamStatus.Pair)
        {
            TeamUtils.SetRole(company, company.cEO.HumanId, WorkerRole.Business, gameContext);
        }

        if (status == TeamStatus.SmallTeam)
        {

        }
    }
}
