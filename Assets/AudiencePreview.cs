using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudiencePreview : View
{
    public Text Loyalty;
    public void SetEntity(AudienceInfo audience)
    {
        base.ViewRender();

        var segmentId = audience.ID;

        var links = GetComponent<ProductUpgradeLinks>();
        links.Title.text = $"<b>{audience.Name}</b>\n";

        var loyalty = Random.Range(-5, 15);

        bool isNewAudience = !Flagship.marketing.ClientList.ContainsKey(segmentId) || Flagship.marketing.ClientList[segmentId] == 0;
        bool isLoyalAudience = loyalty >= 0;

        if (isNewAudience)
        {
            links.Background.color = Visuals.GetColorFromString(Colors.COLOR_NEUTRAL);
            links.Title.text += $"Potential: {Format.MinifyToInteger(audience.Size)} users";
        }

        else
        {
            Loyalty.text = Format.Sign(loyalty);
            if (isLoyalAudience)
            {
                var income = Economy.GetIncomePerSegment(Q, Flagship, segmentId);

                links.Background.color = Visuals.GetColorPositiveOrNegative(true);
                links.Title.text += $"Income: {Format.MinifyMoney(income)}";
            }

            else
            {
                var loss = Marketing.GetChurnClients(Q, Flagship.company.Id, segmentId);

                links.Background.color = Visuals.GetColorPositiveOrNegative(false);
                links.Title.text += $"Loss: {Format.MinifyToInteger(loss)} users";
            }
        }
    }
}
