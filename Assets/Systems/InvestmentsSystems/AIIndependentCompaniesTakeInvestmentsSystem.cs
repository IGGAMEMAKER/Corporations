using Assets.Core;
using System.Collections.Generic;
using System.Linq;

public class AIProcessInvestmentsSystem : OnPeriodChange
{
    public AIProcessInvestmentsSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var company in Companies.GetNonFinancialCompanies(gameContext).Where(c => c.isIndependentCompany))
        {
            foreach (var s in company.shareholders.Shareholders)
            {
                var block = s.Value;


                foreach (var offer in block.Investments)
                {
                    if (offer.RemainingPeriods > 0)
                    {
                        Companies.AddResources(company, offer.Portion);
                        offer.RemainingPeriods--;
                    }
                }
            }
        }
    }
}

public class AIIndependentCompaniesTakeInvestmentsSystem : OnMonthChange
{
    public AIIndependentCompaniesTakeInvestmentsSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in Companies.GetIndependentAICompanies(gameContext))
        {
            //if (Economy.IsCanTakeFastCash(gameContext, e))
            TakeInvestments(e);
        }
    }

    void TakeInvestments(GameEntity company)
    {
        // spawning proposals
        Companies.StartInvestmentRound(gameContext, company);

        if (!Companies.IsInvestmentRoundStarted(company))
            return;

        foreach (var s in Companies.GetInvestmentProposals(company))
        {
            var investorShareholderId = s.ShareholderId;

            var investor = Companies.GetInvestorById(gameContext, investorShareholderId);

            Companies.AcceptInvestmentProposal(gameContext, company, investor);
        }
    }
}