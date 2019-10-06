using Assets.Utils;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AIInvestmentSystems : OnHalfYear
{
    public AIInvestmentSystems(Contexts contexts) : base(contexts)
    {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in CompanyUtils.GetAIProducts(gameContext))
            TakeInvestments(e);
    }

    bool InvestorIsNotRelatedToPlayer (InvestmentProposal proposal)
    {
        var investor = InvestmentUtils.GetInvestorById(gameContext, p.ShareholderId);

        return !(investor.hasCompany && CompanyUtils.IsCompanyRelatedToPlayer(gameContext, investor));
    }

    void TakeInvestments(GameEntity product)
    {
        CompanyUtils.StartInvestmentRound(product, gameContext);

        var list = CompanyUtils.GetInvestmentProposals(gameContext, product.company.Id)
            .Where(InvestorIsNotRelatedToPlayer);

        var companyId = product.company.Id;

        foreach (var s in CompanyUtils.GetInvestmentProposals(gameContext, product.company.Id))
        {
            var investorShareholderId = s.ShareholderId;
            var shareholderName = CompanyUtils.GetInvestorName(gameContext, investorShareholderId);

            CompanyUtils.AcceptProposal(gameContext, companyId, investorShareholderId);

            Format.Print($"Took investments from {shareholderName}. Offer: {Format.Money(s.Offer)}", product);
        }
    }
}