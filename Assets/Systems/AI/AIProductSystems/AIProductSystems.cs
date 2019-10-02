using Assets.Utils;
using System.Collections.Generic;

public partial class AIProductSystems : OnMonthChange
{
    public AIProductSystems(Contexts contexts) : base(contexts) {
    }

    protected override void Execute(List<GameEntity> entities)
    {
        foreach (var e in CompanyUtils.GetProductCompanies(gameContext))
            ManageProductDevelopment(e);

        foreach (var e in CompanyUtils.GetAIProducts(gameContext))
            Operate(e);

        foreach (var e in CompanyUtils.GetPlayerRelatedCompanies(gameContext))
            OperatePlayerRelatedProductCompany(e);
    }

    void OperatePlayerRelatedProductCompany(GameEntity product)
    {
        if (product.isIndependentCompany)
            return;

        var profit = GetProfit(product);
        long dividends = 0;

        if (CompanyUtils.IsCompanyRelatedToPlayer(gameContext, product))
        {
            dividends = profit;
            //Debug.Log("Is company related to player: " + product.company.Name);

            CompanyUtils.PayDividends(gameContext, product, dividends);
            //return;
        }
    }
}