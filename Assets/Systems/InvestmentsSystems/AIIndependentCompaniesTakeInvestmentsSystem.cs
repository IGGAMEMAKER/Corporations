using Assets.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIProcessInvestmentsSystem : OnPeriodChange
{
    public AIProcessInvestmentsSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var c in Companies.GetNonFinancialCompanies(gameContext))
        {
            try
            {
                foreach (var s in c.shareholders.Shareholders)
                {
                    var investorId = s.Key;
                    var block = s.Value;


                    foreach (var offer in block.Investments)
                    {
                        try
                        {
                            if (offer.RemainingPeriods > 0)
                            {
                                Companies.AddMoneyToInvestor(gameContext, c.shareholder.Id, offer.Portion);
                                offer.RemainingPeriods--;
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.Log("Found investments for " + c.company.Name + " but something fucked up");
                            Debug.LogError(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Log("Got error while checking investments of " + c.company.Name);
                Debug.LogError(ex);
            }
        }
    }
}

public class AIIndependentCompaniesTakeInvestmentsSystem : OnPeriodChange
{
    public AIIndependentCompaniesTakeInvestmentsSystem(Contexts contexts) : base(contexts) {}

    protected override void Execute(List<GameEntity> entities)
    {
        //foreach (var e in Companies.GetIndependentAICompanies(gameContext))
        //{
        //    //while (!Economy.IsHasCashOverflow(gameContext, e))
        //}
        foreach (var e in Companies.GetIndependentAICompanies(gameContext))
            TakeInvestments(e);
    }

    bool IsHasMoneyOverflow(GameEntity company)
    {
        var balance = Economy.BalanceOf(company);
        var nonCapitalCost = Economy.GetCompanyBaseCost(gameContext, company.company.Id);

        return balance > 0.3d * nonCapitalCost;
    }

    void TakeInvestments(GameEntity company)
    {
        if (IsHasMoneyOverflow(company))
            return;

        Companies.StartInvestmentRound(gameContext, company);

        if (!Companies.IsInvestmentRoundStarted(company))
            return;

        var companyId = company.company.Id;

        var suitableProposals = Companies.GetInvestmentProposals(gameContext, companyId)
            .Where(InvestorIsNotRelatedToPlayer)
            .Where(p => !IsTargetInvestsInInvestorItself(p, company));

        foreach (var s in suitableProposals)
        {
            var investorShareholderId = s.ShareholderId;

            Companies.AcceptInvestmentProposal(gameContext, companyId, investorShareholderId);

            //var shareholderName = Companies.GetInvestorName(gameContext, investorShareholderId);
            //Format.Print($"Took investments from {shareholderName}. Offer: {Format.Money(s.Offer)}", company);
        }
    }

    bool InvestorIsNotRelatedToPlayer(InvestmentProposal proposal)
    {
        var investor = Investments.GetInvestorById(gameContext, proposal.ShareholderId);

        var isRelatedToPlayer = investor.hasCompany && Companies.IsRelatedToPlayer(gameContext, investor);

        return !isRelatedToPlayer;
    }

    bool IsTargetInvestsInInvestorItself(InvestmentProposal proposal, GameEntity targetCompany)
    {
        var investor = Companies.GetInvestorById(gameContext, proposal.ShareholderId);

        if (!investor.hasCompany || !targetCompany.hasShareholder)
            return false;

        return Companies.IsInvestsInCompany(investor, targetCompany.shareholder.Id);
    }
}