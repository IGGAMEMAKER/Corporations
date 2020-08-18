using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudiencePreview : View
{
    public Text Loyalty;
    public RawImage AudienceImage;
    public Image TargetAudience;

    public void SetEntity(AudienceInfo audience)
    {
        base.ViewRender();

        var segmentId = audience.ID;

        var links = GetComponent<ProductUpgradeLinks>();
        bool isMainAudience = Flagship.productTargetAudience.SegmentId == segmentId;

        links.Title.text = Visuals.Colorize($"<b>{audience.Name}</b>\n", isMainAudience ? Colors.COLOR_GOLD : Colors.COLOR_WHITE);
        Draw(TargetAudience, isMainAudience);

        var loyalty = (int) Marketing.GetSegmentLoyalty(Q, Flagship, segmentId); // Random.Range(-5, 15);

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
                links.Title.text += $"Income: {Visuals.Positive("+" + Format.MinifyMoney(income))}";
            }

            else
            {
                var loss = Marketing.GetChurnClients(Q, Flagship.company.Id, segmentId);

                links.Background.color = Visuals.GetColorPositiveOrNegative(false);
                links.Title.text += $"Loss: {Visuals.Negative(Format.MinifyToInteger(loss))} users";
            }
        }

        AudienceImage.texture = Resources.Load<Texture2D>($"Audiences/{audience.Icon}");
    }
}
