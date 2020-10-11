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
        // calculate startup goal

        // if it's vital interest
        // give them a lot of money

        // otherwise
        // give them decent amount of money

        return 0;
        //var targ  = Products.GetUpgradeCost(product, gameContext, ProductUpgrade.TargetingCampaign);
        //var brand = Products.GetUpgradeCost(product, gameContext, ProductUpgrade.BrandCampaign);

        //return targ + brand;
    }

    void SupportStartup(GameEntity product, long budget)
    {
        //Debug.Log("Support Startup: " + product.company.Name);

        var managingCompany = Companies.GetManagingCompanyOf(product, gameContext);

        // head company bankrupted somehow
        if (managingCompany.company.Id == product.company.Id)
            return;

        var sum = GetMoneyForExpansion(product);

        if (sum < budget)
            SendMoney(product, managingCompany, sum);
    }

    void SendMoney(GameEntity product, GameEntity managingCompany, long sum)
    {
        var proposal = new InvestmentProposal
        {
            Investment = new Investment(sum, 1, InvestorBonus.None, InvestorGoal.GrowCompanyCost),

            ShareholderId = managingCompany.shareholder.Id,
            WasAccepted = false
        };

        Companies.AddInvestmentProposal(gameContext, product, proposal);
        Companies.AcceptInvestmentProposal(gameContext, product, managingCompany.shareholder.Id);
    }
}
