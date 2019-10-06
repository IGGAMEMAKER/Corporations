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


    void TakeInvestments(GameEntity product)
    {
        var list = CompanyUtils.GetPotentialInvestors(gameContext, product.company.Id);

        var investors = string.Join(",", list.Select(l => l.shareholder.Name));

        Format.Print("Take Investments: " + list.Length + " " + investors, product);

        if (list.Length == 0)
            return;

        CompanyUtils.StartInvestmentRound(product, gameContext);

        foreach (var s in list)
        {
            if (s.hasCompany && CompanyUtils.IsCompanyRelatedToPlayer(gameContext, s))
                return;


            var investorShareholderId = s.shareholder.Id;
            var companyId = product.company.Id;

            var proposal = CompanyUtils.GetInvestmentProposal(gameContext, companyId, investorShareholderId);
            //if (proposal == null)
            //    return;


            Format.Print($"Took investments from {s.shareholder.Name}. Offer: {Format.Money(proposal.Offer)}", product);

            CompanyUtils.AcceptProposal(gameContext, companyId, investorShareholderId);
        }
    }
}