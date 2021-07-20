using Assets.Core;
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
