using Assets.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIInvestmentSystem : OnHalfYear
{
    public AIInvestmentSystem(Contexts contexts) : base(contexts)
    {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in Companies.GetAIManagingCompanies(gameContext))
            TakeInvestments(e);

        foreach (var e in Companies.GetAIProducts(gameContext))
            TakeInvestments(e);
    }

    bool InvestorIsNotRelatedToPlayer (InvestmentProposal proposal)
    {
        var investor = Investments.GetInvestorById(gameContext, proposal.ShareholderId);

        var isRelatedToPlayer = investor.hasCompany && Companies.IsCompanyRelatedToPlayer(gameContext, investor);

        return !isRelatedToPlayer;
    }

    bool IsTargetInvestsInInvestorItself (InvestmentProposal proposal, GameEntity targetCompany)
    {
        var investor = Companies.GetInvestorById(gameContext, proposal.ShareholderId);

        if (!investor.hasCompany || !targetCompany.hasShareholder)
            return false;

        bool isTargetInvestsInFutureInvestor = Companies.IsInvestsInCompany(investor, targetCompany.shareholder.Id);

        return isTargetInvestsInFutureInvestor;
    }

    void TakeInvestments(GameEntity company)
    {
        Companies.StartInvestmentRound(company, gameContext);

        var companyId = company.company.Id;

        var suitableProposals = Companies.GetInvestmentProposals(gameContext, companyId)
            .Where(InvestorIsNotRelatedToPlayer)
            .Where(p => !IsTargetInvestsInInvestorItself(p, company));

        foreach (var s in suitableProposals)
        {
            var investorShareholderId = s.ShareholderId;
            var shareholderName = Companies.GetInvestorName(gameContext, investorShareholderId);

            Companies.AcceptProposal(gameContext, companyId, investorShareholderId);

            //Format.Print($"Took investments from {shareholderName}. Offer: {Format.Money(s.Offer)}", company);
        }
    }
}