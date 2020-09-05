using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AudiencesOnMainScreenListView : ListView
{
    public ProductUpgradeLinks MainAudienceInfo;
    public ProductUpgradeLinks AmountOfUsers;
    public ProductUpgradeLinks Potential;
    public ProductUpgradeLinks MainInfo;
    public ProductUpgradeLinks LoyaltyInfo;

    public GameObject ButtonList;

    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        t.GetComponent<AudiencePreview>().SetEntity((AudienceInfo)(object)entity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var audiences = Marketing.GetAudienceInfos();

        if (Flagship.isRelease)
        {
            SetItems(audiences);
        }
        else
        {
            // take primary audience only
            SetItems(audiences.Where(a => a.ID == Flagship.productTargetAudience.SegmentId));
        }
    }

    public override void OnDeselect()
    {
        base.OnDeselect();

        HideButtons();

        var a = FindObjectOfType<MainPanelRelay>();
        if (a != null)
            a.ShowDefaultMode();
    }

    public void HideButtons()
    {
        Hide(ButtonList);
    }

    public override void OnItemSelected(int ind)
    {
        base.OnItemSelected(ind);

        FindObjectOfType<MainPanelRelay>().ExpandAudiences();

        Show(ButtonList);

        var segmentId = Items[ind].GetComponent<AudiencePreview>().Audience.ID;

        bool isTargetAudience   = Flagship.productTargetAudience.SegmentId == segmentId;
        bool hasClients         = Flagship.marketing.ClientList.ContainsKey(segmentId);
        var clients             = hasClients ? Flagship.marketing.ClientList[segmentId] : 0;

        Draw(MainAudienceInfo, isTargetAudience);
        Draw(AmountOfUsers, clients > 0);

        RenderAudienceData(segmentId, clients);
    }

    void RenderAudienceData(int segmentId, long clients)
    {
        var audience = Marketing.GetAudienceInfos()[segmentId];

        var growing = Marketing.GetAudienceGrowthBySegment(Flagship, Q, segmentId);

        var incomePerUser = 0.42f;
        var worth = (long)((double)audience.Size * incomePerUser);

        var income = Economy.GetIncomePerSegment(Q, Flagship, segmentId);

        var potentialPhrase = Format.Minify(audience.Size);
        var marketWorth = Format.MinifyMoney(worth);

        var growthPhrase = $"+{Format.Minify(growing)} weekly";

        var loyalty = Marketing.GetSegmentLoyalty(Q, Flagship, segmentId, true);

        MainInfo.Title.text          = $"<b>{audience.Name}</b>\nIncome: {Visuals.Positive(Format.MinifyMoney(income))}";
        AmountOfUsers.Title.text     = $"{Format.Minify(clients)} {audience.Name}\n" + Visuals.Colorize(growthPhrase, growing >= 0);

        MainAudienceInfo.Title.text  = "<b>Our main audience</b>";
        MainAudienceInfo.Title.color = Visuals.GetColorFromString(Colors.COLOR_GOLD);

        Potential.Title.text         = $"<b>Potential: {potentialPhrase} users</b>\nworth {marketWorth}";

        LoyaltyInfo.Title.text       = $"<b>Loyalty</b>\n";
        LoyaltyInfo.Hint.SetHint(loyalty.SortByModule(true).ToString());

        var isLoyal = loyalty.Sum() >= 0;
        if (isLoyal)
        {
            LoyaltyInfo.Title.text += Visuals.Positive("+" + (int)loyalty.Sum());
        }
        else
        {
            var worstValue = loyalty.bonusDescriptions.Min(b => b.Value);
            var worstThing = loyalty.bonusDescriptions.Find(b => b.Value == worstValue);

            LoyaltyInfo.Title.text += Visuals.Negative($"{worstThing.Name}: {worstThing.Value}");
        }
    }

    public void ShowAudienceLoyaltyChangeOnFeatureUpgrade(NewProductFeature f)
    {
        foreach (var preview in Items)
        {
            preview.GetComponent<AudiencePreview>().ShowChanges(f);
        }
    }

    public void HideLoyaltyChanges()
    {
        foreach (var preview in Items)
        {
            preview.GetComponent<AudiencePreview>().HideLoyaltyChanges();
        }
    }
}
