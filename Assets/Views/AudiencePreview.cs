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

    public int segmentId;

    public void SetEntity(AudienceInfo audience)
    {
        base.ViewRender();

        segmentId = audience.ID;
        Audience = audience;

        var links = GetComponent<ProductUpgradeLinks>();
        bool isMainAudience = Marketing.IsTargetAudience(Flagship, Q, segmentId);

        if (links != null)
            links.Title.text = Visuals.Colorize($"<b>{audience.Name}</b>\n", isMainAudience ? Colors.COLOR_GOLD : Colors.COLOR_WHITE);

        Draw(TargetAudience, isMainAudience);

        var loyalty = (int) Marketing.GetSegmentLoyalty(Q, Flagship, segmentId); // Random.Range(-5, 15);
        var loyaltyBonus = Marketing.GetSegmentLoyalty(Q, Flagship, segmentId, true);

        long clients = Flagship.marketing.ClientList.ContainsKey(segmentId) ? Flagship.marketing.ClientList[segmentId] : 0;

        bool isNewAudience = clients == 0;
        bool isLoyalAudience = loyalty >= 0;

        var text = $"<size=35>{audience.Name}</size>";

        if (isNewAudience)
        {
            if (links != null)
            {
                links.Background.color = Visuals.GetColorFromString(Colors.COLOR_NEUTRAL);
                links.Title.text += $"Potential: {Format.MinifyToInteger(audience.Size)} users";
            }

            text += $"\n\nIncome: <b>{Visuals.Positive("???")}</b>" +
                $"\nPotential: <b>{Visuals.Positive("???")}</b>";
        }

        else
        {
            Loyalty.text = Format.Sign(loyalty);
            Loyalty.GetComponent<Hint>().SetHint(loyaltyBonus.SortByModule(true).ToString());

            var income = Economy.GetIncomePerSegment(Q, Flagship, segmentId);
            if (isLoyalAudience)
            {
                if (links != null)
                {
                    links.Background.color = Visuals.GetColorPositiveOrNegative(true);
                    links.Title.text += $"Income: {Visuals.Positive("+" + Format.MinifyMoney(income))}";
                }
            }

            else
            {
                var loss = Marketing.GetChurnClients(Q, Flagship.company.Id, segmentId);

                if (links != null)
                {
                    links.Background.color = Visuals.GetColorPositiveOrNegative(false);
                    links.Title.text += $"Loss: {Visuals.Negative(Format.MinifyToInteger(loss))} users";
                }
            }

            text += $"\n\nIncome: <b>{Visuals.Colorize(Format.MinifyMoney(income), income >= 0)}</b>" +
                $"\nUsers: <b>{Visuals.Colorize(Format.Minify(clients), clients >= 0)}</b>";
        }

        AudienceImage.texture = Resources.Load<Texture2D>($"Audiences/{audience.Icon}");

        var potentialPhrase = $"{Format.Minify(audience.Size)} users";
        //var incomePerUserPhrase = $"${income.ToString("0.0")}";

        //$"\n\nIncome per user: <b>{Visuals.Positive(incomePerUserPhrase)}</b>" +
        //$"\nPotential audience: <b>{Visuals.Positive(potentialPhrase)}</b>"

        AudienceHint.SetHint(text);
        HideLoyaltyChanges();
    }

    public void ShowChanges(NewProductFeature f)
    {
        int change = (int)Marketing.GetLoyaltyChangeFromFeature(Flagship, f, segmentId, true);

        ShowChanges(change);
    }

    public void ShowChanges(int change)
    {
        LoyaltyChange.text = Format.Sign(change);
        LoyaltyChange.color = Visuals.GetColorPositiveOrNegative(change >= 0);

        Show(LoyaltyChangeBackground);
    }

    public void HideLoyaltyChanges()
    {
        Hide(LoyaltyChangeBackground);
    }
}
