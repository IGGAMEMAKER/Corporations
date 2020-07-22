using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestorPreview : View
{
    public void SetEntity(int shareholderId, GameEntity company)
    {
        ProductUpgradeLinks productUpgradeLinks = GetComponent<ProductUpgradeLinks>();

        if (productUpgradeLinks == null)
            return;

        var investor = Investments.GetInvestorById(Q, shareholderId);

        var isPlayer = investor.isPlayer;

        bool isLoyal = Random.Range(-15, 15) > 0;

        var shares = Companies.GetShareSize(Q, company.company.Id, shareholderId);
        var goal = "Get most users";

        string name = isPlayer ? "YOU" : investor.shareholder.Name;

        productUpgradeLinks.Title.text = $"<b>{name}</b>\n{shares}% shares\n{goal}";

        if (isPlayer)
        {
            productUpgradeLinks.Background.color = Visuals.GetColorFromString(Colors.COLOR_GOLD);
        }
        else
        {
            productUpgradeLinks.Background.color = Visuals.GetColorPositiveOrNegative(isLoyal);
        }
    }
}
