﻿using Assets.Core;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MarketingChannelView : View, IPointerEnterHandler, IPointerExitHandler
{
    public GameEntity channel;

    public Text Title;
    public Text Users;
    public Text Income;

    public Text CostPerUser;

    public CanvasGroup CanvasGroup;
    public Image ChosenImage;

    public Image BackgroundImage; // 060F30

    public RawImage SegmentTypeImage;

    public Image ChosenCheckMark;

    public Image ExplorationImage;
    public Image DomineeringIcon;

    public Text MarketingComplexity;

    float maxROI = 100;
    float minROI = 0;

    RenderAudiencesListView RenderAudiencesListView;

    bool isFreeChannel = false;

    public override void ViewRender()
    {
        base.ViewRender();

        // some error
        if (channel == null)
            return;

        ToggleTexts(false);

        var marketingChannel = channel.marketingChannel;

        var channel1 = marketingChannel.ChannelInfo;

        var company = Flagship;


        // basic info
        var name = $"Channel {channel1.ID}";
        Title.text = name;

        var adCost = Marketing.GetChannelCost(company, channel);
        var ROI = Marketing.GetChannelCostPerUser(company, Q, channel);
        var repaymentColor = Visuals.GetGradientColor(minROI, maxROI, ROI, true);

        isFreeChannel = adCost == 0;
        if (isFreeChannel)
            ToggleTexts(true);

        // audience gain
        var gainedAudience = Marketing.GetChannelClientGain(company, channel);
        Users.text = "+" + Format.Minify(gainedAudience) + " users";
        //Users.color = repaymentColor;

        // audience icon
        if (Marketing.IsFocusingMoreThanOneAudience(company))
        {
            SegmentTypeImage.texture = Resources.Load<Texture2D>($"paidClients");
        }
        else
        {
            var segmentID = Marketing.GetCoreAudienceId(company);
            var audiences = Marketing.GetAudienceInfos();

            SegmentTypeImage.texture = Resources.Load<Texture2D>($"Audiences/{audiences[segmentID].Icon}");
        }

        var canMaintain = Economy.IsCanMaintainForAWhile(MyCompany, Q, adCost, 1);
        //BackgroundImage.color = canMaintain ? Visuals.Positive() : Visuals.Negative();

        var clientCost = ROI; // adCost / gainedAudience;

        Income.text = $"{Format.MinifyMoney(adCost)}"; //  (${clientCost.ToString("0.00")} each)
        if (isFreeChannel)
            Income.text = "FREE";
        //else
        //    Income.color = repaymentColor;

        Income.color = Visuals.GetColorPositiveOrNegative(canMaintain);
        CostPerUser.text = $"{clientCost.ToString("0.0")}$";
        CostPerUser.color = repaymentColor;

        MarketingComplexity.text = $"{clientCost.ToString("0")}$";
        MarketingComplexity.color = repaymentColor;

        bool isActiveChannel = Marketing.IsActiveInChannel(company, channel);
        CanvasGroup.alpha = 1;

        Draw(ChosenImage, isActiveChannel);
        Draw(ChosenCheckMark, isActiveChannel);

        var dayOfPeriod = CurrentIntDate % C.PERIOD;
        RenderProgress(isActiveChannel ? dayOfPeriod : 0, C.PERIOD);
    }

    void RenderProgress(float progress, float duration)
    {
        ExplorationImage.fillAmount = 1f - (duration - progress) / duration; // Random.Range(0, 1f);
    }

    public void SetEntity(GameEntity channel, float minROI, float maxROI, RenderAudiencesListView RenderAudiencesListView)
    {
        this.channel = channel;
        this.maxROI = maxROI;
        this.minROI = minROI;
        this.RenderAudiencesListView = RenderAudiencesListView;

        ViewRender();
    }

    void ToggleTexts(bool showCost)
    {
        Draw(MarketingComplexity, false);
        Draw(Income, true); // showCost

        //Draw(SegmentTypeImage, !showFace);
        //Draw(Users, !showFace);
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        ToggleTexts(true);

        var audiences = Marketing.GetAudienceInfos();

        var changes = audiences.Select(a => Marketing.GetChannelClientGain(Flagship, channel, a.ID));
        if (RenderAudiencesListView != null)
            RenderAudiencesListView.ShowValueChanges(changes.ToList());
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (!isFreeChannel)
            ToggleTexts(false);

        if (RenderAudiencesListView != null)
            RenderAudiencesListView.HideChanges();
    }
}
