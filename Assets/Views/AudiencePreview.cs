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

        Draw(TargetAudience, isMainAudience);

        var loyalty = (int) Marketing.GetSegmentLoyalty(Q, Flagship, segmentId); // Random.Range(-5, 15);
        var loyaltyBonus = Marketing.GetSegmentLoyalty(Q, Flagship, segmentId, true);

        long clients = Flagship.marketing.ClientList.ContainsKey(segmentId) ? Flagship.marketing.ClientList[segmentId] : 0;

        bool isNewAudience = clients == 0;
        bool isLoyalAudience = loyalty >= 0;

        var text = $"<size=35>{audience.Name}</size>";

        if (links != null)
        {
            if (isNewAudience)
                links.Background.color = Visuals.GetColorFromString(Colors.COLOR_NEUTRAL);
            else
                links.Background.color = Visuals.GetColorPositiveOrNegative(isLoyalAudience);
        }


        if (isNewAudience)
        {
            text += $"\n\nIncome: <b>{Visuals.Positive("???")}</b>" +
                $"\nPotential: <b>{Visuals.Positive("???")}</b>";

            Hide(Loyalty);
        }

        else
        {
            Show(Loyalty);

            Loyalty.text = Format.Sign(loyalty);
            Loyalty.GetComponent<Hint>().SetHint(loyaltyBonus.SortByModule(true).RenderTitle().ToString());

            var income = Economy.GetIncomePerSegment(Q, Flagship, segmentId);

            text += $"\n\nIncome: <b>{Visuals.Colorize(Format.MinifyMoney(income), income >= 0)}</b>" +
                $"\nUsers: <b>{Visuals.Colorize(Format.Minify(clients), clients >= 0)}</b>";
        }

        AudienceImage.texture = Resources.Load<Texture2D>($"Audiences/{audience.Icon}");

        var potentialPhrase = $"{Format.Minify(audience.Size)} users";

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
