using System.Collections.Generic;
using System.Linq;
using Assets.Core;
using UnityEngine;

public partial class AISupportProductsSystem : OnPeriodChange
{
    public AISupportProductsSystem(Contexts contexts) : base(contexts) { }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var m in Companies.GetAIManagingCompanies(gameContext))
        {
            var products = Companies.GetDaughterProductCompanies(gameContext, m);

            var importances = products
                .Select(p => Companies.GetMarketImportanceForCompany(gameContext, m, p.product.Niche))
                .ToArray();

            var sum = importances.Sum();
            if (sum == 0)
                sum = 1;

            var balance = Economy.BalanceOf(m);

            for (var i = 0; i < products.Length; i++)
            {
                var budget = balance * importances[i] / sum;

                SupportStartup(products[i], budget);
            }
        }
    }

    public long GetMoneyForExpansion(GameEntity product)
    {
        var next = Economy.GetNextMarketingLevelCost(product, gameContext);
        var curr = Economy.GetMarketingCost(product, gameContext);

        var diff = next - curr;

        return diff;
    }

    void SupportStartup(GameEntity product, long budget)
    {
        Debug.Log("Support Startup");

        var managingCompany = Companies.GetManagingCompanyOf(product, gameContext);


        // calculate startup goal

        // if it's vital interest
        // give them a lot of money

        // otherwise
        // give them decent amount of money
        var sum = GetMoneyForExpansion(product);

        if (sum < budget)
            SendMoney(product, managingCompany, sum);
    }

    void SendMoney(GameEntity product, GameEntity managingCompany, long sum)
    {
        var proposal = new InvestmentProposal
        {
            Offer = sum,
            ShareholderId = managingCompany.shareholder.Id,
            InvestorBonus = InvestorBonus.None,
            Valuation = 0,
            WasAccepted = false
        };

        Companies.AddInvestmentProposal(gameContext, product.company.Id, proposal);
        Companies.AcceptInvestmentProposal(gameContext, product.company.Id, managingCompany.shareholder.Id);
    }
}
