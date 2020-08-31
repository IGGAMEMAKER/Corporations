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

    public Hint AudienceHint;

    public AudienceInfo Audience;

    public Text LoyaltyChange;
    public GameObject LoyaltyChangeBackground;

    int segmentId;

    public void SetEntity(AudienceInfo audience)
    {
        base.ViewRender();

        segmentId = audience.ID;
        Audience = audience;

        var links = GetComponent<ProductUpgradeLinks>();
        bool isMainAudience = Flagship.productTargetAudience.SegmentId == segmentId;

        links.Title.text = Visuals.Colorize($"<b>{audience.Name}</b>\n", isMainAudience ? Colors.COLOR_GOLD : Colors.COLOR_WHITE);
        Draw(TargetAudience, isMainAudience);

        var loyalty = (int) Marketing.GetSegmentLoyalty(Q, Flagship, segmentId); // Random.Range(-5, 15);
        var loyaltyBonus = Marketing.GetSegmentLoyalty(Q, Flagship, segmentId, true);

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
            Loyalty.GetComponent<Hint>().SetHint(loyaltyBonus.SortByModule(true).ToString());

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

        var incomePerUser = Economy.GetIncomePerUser(Q, Flagship, segmentId);
        var worth = (long)((double)audience.Size * incomePerUser);


        //var income = Economy.GetIncomePerSegment(Q, Flagship, segmentId);

        //MainInfo.Title.text = $"<b>{audience.Name}</b>\nIncome: {Visuals.Positive("+" + Format.MinifyMoney(income))}";

        var potentialPhrase = $"{Format.Minify(audience.Size)} users";
        var incomePerUserPhrase = $"${incomePerUser.ToString("0.0")}";
        var growthBonus = "???";

        var text = $"<size=35>{audience.Name}</size>" +
            $"\n\nPotential: <b>{Visuals.Positive(potentialPhrase)}</b>" + //  (worth {Format.MinifyMoney(worth)})
            $"\n\nIncome per user: <b>{Visuals.Positive(incomePerUserPhrase)}</b>" +
            $"\nGrowth speed: <b>{Visuals.Positive(growthBonus)}</b>" +
            $"\n\nLoyalty: <b>{Visuals.Colorize(Format.Sign(loyalty), isLoyalAudience)}</b>";

        AudienceHint.SetHint(text);
        HideLoyaltyChanges();
    }

    public void ShowChanges(NewProductFeature f)
    {
        int change = (int)Marketing.GetLoyaltyChangeFromFeature(Flagship, f, segmentId, true);

        LoyaltyChange.text = Format.Sign(change);
        LoyaltyChange.color = Visuals.GetColorPositiveOrNegative(change >= 0);

        Draw(LoyaltyChangeBackground, change != 0);
    }

    public void HideLoyaltyChanges()
    {
        Hide(LoyaltyChangeBackground);
    }
}
