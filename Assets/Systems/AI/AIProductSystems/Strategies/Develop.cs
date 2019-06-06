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

        return;


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
        if (TeamUtils.IsWillOverextendTeam(company))
        {
            UpgradeTeam(company);
        }
        else
        {
            HireWorker(company, GetProperWorkerRole(company));
        }
    }

    void HireWorker(GameEntity company, WorkerRole workerRole)
    {
        TeamUtils.HireWorker(company, workerRole);

        Print($"Hire {workerRole.ToString()}", company);
    }

    void UpgradeTeam(GameEntity company)
    {
        var status = company.team.TeamStatus;

        TeamUtils.Promote(company);

        Print($"Upgrade team from {status.ToString()}", company);

        if (status == TeamStatus.Pair)
        {
            Print($"Set universal worker as CEO", company);

            TeamUtils.SetRole(company, company.cEO.HumanId, WorkerRole.Business, gameContext);
        }

        if (status == TeamStatus.SmallTeam)
        {

        }
    }
}
