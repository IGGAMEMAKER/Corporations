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

    public Text Iteration;
    public Text FeatureCap;

    public GameObject ButtonList;

    public override void SetItem<T>(Transform t, T entity)
    {
        t.GetComponent<AudiencePreview>().SetEntity((AudienceInfo)(object)entity, product);
    }

    GameEntity product => CurrentScreen == ScreenMode.ProjectScreen ? SelectedCompany : Flagship;
    public override void ViewRender()
    {
        base.ViewRender();

        var audiences = Marketing.GetAudienceInfos();

        if (!product.hasProduct)
            return;

        //bool showAudiences = true;
        bool showAudiences = product.isRelease;

        if (showAudiences)
        {
            SetItems(audiences);
        }
        else
        {
            // take primary audience only
            // positioningId will be always less than amount of audiences
            SetItems(audiences.Where(a => a.ID == Marketing.GetCoreAudienceId(product)));
        }

        Teams.UpdateTeamEfficiency(product, Q);

        if (Iteration != null)
            Iteration.text = Visuals.Positive(Products.GetIterationTime(product) + " days");

        if (FeatureCap != null)
            FeatureCap.text = Visuals.Positive(Products.GetFeatureRatingCap(product).ToString("0.0"));
    }

    public override void OnDeselect(int ind)
    {
        base.OnDeselect(ind);

        HideButtons();
    }

    public void HideButtons()
    {
        Hide(ButtonList);
    }

    // TODO REMOVE
    void RenderAudienceData(int segmentId, long clients, GameEntity product)
    {
        var audience = Marketing.GetAudienceInfos()[segmentId];

        var growing = Marketing.GetAudienceGrowthBySegment(product, Q, segmentId);

        var incomePerUser = 0.42f;
        var worth = (long)((double)audience.Size * incomePerUser);

        var income = Economy.GetIncomePerSegment(product, segmentId);

        var potentialPhrase = Format.Minify(audience.Size);
        var marketWorth = Format.MinifyMoney(worth);

        var growthPhrase = $"+{Format.Minify(growing)} weekly";

        var loyalty = Marketing.GetSegmentLoyalty(product, segmentId, true);

        MainInfo.Title.text          = $"<b>{audience.Name}</b>\nIncome: {Visuals.Positive(Format.MinifyMoney(income))}";
        AmountOfUsers.Title.text     = $"{Format.Minify(clients)} {audience.Name}\n" + Visuals.Colorize(growthPhrase, growing >= 0);

        MainAudienceInfo.Title.text  = "<b>Our main audience</b>";
        MainAudienceInfo.Title.color = Visuals.GetColorFromString(Colors.COLOR_GOLD);

        //Potential.Title.text         = $"<b>Potential: {potentialPhrase} users</b>\nworth {marketWorth}";

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

    public void ShowLoyaltyChanges(List<int> changes)
    {
        foreach (var preview in Items)
        {
            var audPreview = preview.GetComponent<AudiencePreview>();

            audPreview.ShowChanges(changes[audPreview.segmentId]);
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
