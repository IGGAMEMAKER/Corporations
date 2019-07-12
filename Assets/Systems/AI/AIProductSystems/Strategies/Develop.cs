using Assets.Utils;

public interface IAIProductCompany
{
    void Crunch(GameEntity company);

    void IncreasePrices(GameEntity company);
    void DecreasePrices(GameEntity company);

    void UpgradeTeam(GameEntity company);
    void ExpandTeam(GameEntity company);
    void ShrinkTeam(GameEntity company);

    void ImproveSegment(GameEntity company);
    void ImprovePayment(GameEntity company);

    void StartTargetingCampaign(GameEntity company);
    void StartTestCampaign(GameEntity company);
    void StartBranding(GameEntity company);
    void ReleaseApp(GameEntity company);

    void UpdatePositioning(GameEntity company);
}

public partial class AIProductSystems : OnDateChange
{
    void CompleteCompanyGoal(GameEntity company)
    {
        switch (company.companyGoal.InvestorGoal)
        {
            case InvestorGoal.Prototype: Prototype(company); break;
            case InvestorGoal.FirstUsers: GrabFirstUsers(company); break;
            case InvestorGoal.Release: Release(company); break;

            case InvestorGoal.BecomeMarketFit: BecomeMarketFit(company); break;
            case InvestorGoal.BecomeProfitable: BecomeProfitable(company); break;

            //case InvestorGoal.GrowCompanyCost: GrowCompanyCost(company); break;
            //case InvestorGoal.IPO: GrowCompanyCost(company); break;

            case InvestorGoal.Operationing: Operate(company); break;
        }

        InvestmentUtils.CompleteGoal(company, gameContext, false);

        return;
    }
}
