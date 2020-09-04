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
        foreach (var company in Companies.GetNonFinancialCompanies(gameContext))
        {
            if (!company.isIndependentCompany) continue;
            try
            {
                foreach (var s in company.shareholders.Shareholders)
                {
                    var investorId = s.Key;
                    var block = s.Value;


                    foreach (var offer in block.Investments)
                    {
                        try
                        {
                            if (offer.RemainingPeriods > 0)
                            {
                                Companies.AddResources(company, offer.Portion);
                                offer.RemainingPeriods--;
                            }
                        }
                        catch (Exception ex)
                        {
                            Debug.Log("Found investments for " + company.company.Name + " but something fucked up");
                            Debug.LogError(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Log("Got error while checking investments of " + company.company.Name);
                Debug.LogError(ex);
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
            if (!Economy.IsHasCashOverflow(gameContext, e))
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

            Companies.AcceptInvestmentProposal(gameContext, company.company.Id, investorShareholderId);

            //var shareholderName = Companies.GetInvestorName(gameContext, investorShareholderId);
            //Format.Print($"Took investments from {shareholderName}. Offer: {Format.Money(s.Offer)}", company);
        }
    }
}