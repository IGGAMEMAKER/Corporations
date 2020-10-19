using Assets.Core;
using System.Linq;
using UnityEngine;

public class InvestorsWhoAreInterestedInThisMarketListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        var fund = entity as GameEntity;
        t.GetComponent<MockText>().SetEntity(fund.company.Name);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var niche = SelectedNiche;

        var funds = Companies.GetInvestmentFundsWhoAreInterestedInMarket(Q, niche);

        SetItems(funds);
    }
}
