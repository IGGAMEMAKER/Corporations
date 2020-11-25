using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvestorPreview : View
{
    public int ShareholderId;

    public GameEntity Investor => Investments.GetInvestor(Q, ShareholderId);

    public void SetEntity(int shareholderId, GameEntity company)
    {
        ProductUpgradeLinks productUpgradeLinks = GetComponent<ProductUpgradeLinks>();

        if (productUpgradeLinks == null)
            return;


        ShareholderId = shareholderId;

        var investor = Investor;

        var isPlayer = investor.isPlayer;


        var shares = Companies.GetShareSize(Q, company, investor);
        var goal = "Get most users";

        string name = isPlayer ? "YOU" : investor.shareholder.Name;


        productUpgradeLinks.Title.text = $"<b>{name}</b>\n{shares}% shares\n{goal}";


        if (isPlayer)
        {
            productUpgradeLinks.Background.color = Visuals.GetColorFromString(Colors.COLOR_GOLD);
        }
        else
        {
            bool isLoyal = true; // Random.Range(-15, 15) > 0;
            productUpgradeLinks.Background.color = Visuals.GetColorPositiveOrNegative(isLoyal);
        }
    }
}
