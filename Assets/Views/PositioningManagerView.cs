using Assets.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PositioningManagerView : View
{
    public ProductPositioning Positioning;

    public Text SegmentDescription;

    public CompaniesFocusingSpecificSegmentListView CompaniesFocusingSpecificSegmentListView;
    public RenderAudienceChoiceListView RenderAudienceChoiceListView;

    public GameObject PivotText;
    public GameObject PivotButton;

    public GameObject ExpandText;
    public GameObject ExpandButton;

    public GameObject ORText;

    List<GameObject> PivotButtons => new List<GameObject> { PivotButton, PivotText, ORText, ExpandButton, ExpandText };
    List<GameObject> PivotDescriptions => new List<GameObject> { PivotText, ExpandText };

    public GameObject PositioningsList;
    public GameObject PositioningDescriptionTab;

    public AudienceListView HatedAudiences;
    public Text HatedLabel;
    public AudienceListView LovingAudiences;
    public Text LovingLabel;

    public Text NewPositioningName;
    public Text ChangeGain;

    public GameObject COMPETITION_PANEL;

    public GameObject ChangePositioningButton;

    List<int> TargetAudiences;

    bool flag = false;

    private void OnEnable()
    {
        var audiences = Marketing.GetAudienceInfos();

        Positioning = Marketing.GetPositioning(Flagship);
        TargetAudiences = audiences.Where(a => Marketing.IsAimingForSpecificAudience(Flagship, a.ID)).Select(a => a.ID).ToList();
        flag = true;

        CleanScreen();

        ViewRender();
    }

    void CleanScreen()
    {
        ShowAll(PivotButtons);
        Hide(PositioningsList);
        Hide(COMPETITION_PANEL);

        Hide(ChangePositioningButton);
        //Hide(PositioningDescriptionTab);
    }

    private void OnDisable()
    {
        ChangeGain.text = "";
        CleanScreen();
    }

    //public override void ViewRender()
    public void Rndr()
    {
        if (!flag)
            return;

        var company = Flagship;

        RenderAudienceChoiceListView.ViewRender();
        CompaniesFocusingSpecificSegmentListView.SetSegment(Positioning);

        RenderSegmentDescription(company);
    }

    string DescribeAudience(long cost, int i) => Visuals.Negative($"{Marketing.GetAudienceInfos()[i].Name} ({Format.MinifyMoney(cost)})");

    void RenderSegmentDescription(GameEntity company)
    {
        var audiences = Marketing.GetAudienceInfos();

        var audiencesSelected = Positioning.Loyalties
            .Select((l, i) => new { i, isLoyal = l >= 0 });

        //Debug.Log()

        NewPositioningName.text = Positioning.name;

        var lovingAudiences = audiencesSelected.Where(f => f.isLoyal && !Marketing.IsAimingForSpecificAudience(company, f.i));
        //var lovingAudiences     = audiencesSelected.Where(f => f.isLoyal); //  && !Marketing.IsAimingForSpecificAudience(company, f.i)
        var hatedAudiences = audiencesSelected.Where(f => !f.isLoyal && Marketing.IsWillSufferOnAudienceLoss(company, f.i));
        //var hatedAudiences      = audiencesSelected.Where(f => !f.isLoyal); // && Marketing.IsWillSufferOnAudienceLoss(company, f.i)


        HatedAudiences  .SetAudiences(hatedAudiences.Select(a => audiences[a.i]).ToList(), -1);
        LovingAudiences .SetAudiences(lovingAudiences.Select(a => audiences[a.i]).ToList(), 1);

        bool hasNewLovingUsers = lovingAudiences.Count() > 0;
        bool hasNewHatingUsers = hatedAudiences.Count() > 0;

        Draw(HatedLabel, hasNewHatingUsers);
        Draw(HatedAudiences, hasNewHatingUsers);

        Draw(LovingLabel, hasNewLovingUsers);
        Draw(LovingAudiences, hasNewLovingUsers);

        HatedLabel.text = $"<size=50><color=red><b>!!! You will LOSE ALL of these users</b></color></size>";

        if (hasNewHatingUsers)
            LovingLabel.text = $"Instead, you will <b><color=green>GET</color> these users</b>";
        else
            LovingLabel.text = $"You will <b><color=green>GET</color> these users</b>";

        RenderPositioningChangeBenefit();
    }

    void RenderPositioningChangeBenefit()
    {
        var product = Flagship;
        var audienceChange = (double)Marketing.GetAudienceChange(product, Q);


        var audienceGrowth = (double)Marketing.GetAudienceGrowth(product, Q);

        // Fake positioning change -------------------
        var positioning = Marketing.GetPositioning(product);

        Marketing.ChangePositioning(product, Positioning.ID);
        var newAudienceGrowth = (double)Marketing.GetAudienceGrowth(product, Q);

        var newAppQuality = Marketing.GetAppQuality(product);

        var companies = Companies.GetCompetitionInSegment(product, Q, Positioning.ID, true);
        var newBestAppQuality = companies.Select(c => Marketing.GetAppQuality(c)).Max();

        //var newBestAppQuality = Marketing
        Marketing.ChangePositioning(product, positioning.ID);
        // --------------------------------

        var incomePerUser = Economy.GetIncomePerUser(product, 0) * C.PERIOD / 30; //  0.05d;
        var newIncomePerUser = incomePerUser;

        bool noCompetitors = companies.Count() == 0;

        if (noCompetitors)
        {
            newAudienceGrowth *= 2;
        }


        var incomeGrowth = Convert.ToInt64((audienceGrowth * incomePerUser));
        var newIncomeGrowth = Convert.ToInt64(newAudienceGrowth * newIncomePerUser);


        var situation = $"Your income grows by {Format.Money(incomeGrowth)} every week (by getting {Format.Minify(audienceGrowth)} users).";

        ChangeGain.text = situation;

        var incomeChange = newIncomeGrowth - incomeGrowth;
        var audienceGrowthChange = newAudienceGrowth - audienceGrowth;

        if (newAudienceGrowth != audienceGrowth)
        {
            ChangeGain.text += $"\nAfter positioning change you will ";

            if (newAudienceGrowth > audienceGrowth)
            {
                var incomeGainDescription = "+" + Format.Money(incomeChange) + " / week";
                var audienceGainDescription = "+" + Format.Minify(audienceGrowthChange) + " users";

                ChangeGain.text += $"<b>GET</b> additional <b>{Visuals.Positive(incomeGainDescription)}</b> (by getting <b>additional</b> {Visuals.Positive(audienceGainDescription)})";
            }

            if (newAudienceGrowth < audienceGrowth)
            {
                var incomeGainDescription = Format.Money(-incomeChange) + " / week";
                var audienceGainDescription = Format.Minify(-audienceGrowthChange) + " users";

                ChangeGain.text += $"<b>LOSE</b> <b>{Visuals.Negative(incomeGainDescription)}</b> (by losing {Visuals.Negative(audienceGainDescription)})";
            }
        }
        else
        {
            ChangeGain.text += Visuals.Colorize("\nThis is our current positioning", Colors.COLOR_CONTROL);
        }


        // Competition --------------------
        if (newBestAppQuality > newAppQuality + 5)
        {
            ChangeGain.text += "\n" + Visuals.Negative("Your product is worse than products, which are competing in this segment, so you will need to upgrade more features quickly");
        }

        if (noCompetitors)
        {
            ChangeGain.text += "\n" + Visuals.Positive("There are NO competitors, so you will get <b>TWICE</b> more users!");
        }
        // -------------------------
        //if (newAudienceGrowth == audienceGrowth)
        //{
        //    ChangeGain.text += $""
        //}
    }

    void ShowTabs()
    {
        Show(PositioningsList);
        //Show(PositioningDescriptionTab);
    }

    void HideButtonDescriptions()
    {
        HideAll(PivotDescriptions);
    }

    public void OnExpandButton()
    {
        HideButtonDescriptions();
        ShowTabs();

        FindObjectOfType<RenderAudienceChoiceListView>().SetExpansionPositionings();
        Hide(COMPETITION_PANEL);
    }

    public void OnPivotButton()
    {
        HideButtonDescriptions();
        ShowTabs();

        FindObjectOfType<RenderAudienceChoiceListView>().SetPivotPositionings();
        Hide(COMPETITION_PANEL);
        //Show(PositioningDescriptionTab);
    }

    public void SetAnotherPositioning(ProductPositioning positioning)
    {
        Positioning = positioning;
        flag = true;

        //ViewRender();
        CompaniesFocusingSpecificSegmentListView.SetSegment(Positioning);
        Show(COMPETITION_PANEL);


        Draw(ChangePositioningButton, Positioning.ID != Flagship.productPositioning.Positioning);

        RenderSegmentDescription(Flagship);
    }

    public void AddAudience(int segmentId)
    {
        if (!TargetAudiences.Contains(segmentId))
        {
            TargetAudiences.Add(segmentId);
        }
    }

    public void RemoveAudience(int segmentId)
    {
        if (TargetAudiences.Contains(segmentId))
        {
            TargetAudiences.RemoveAll(id => id == segmentId);
        }
    }


    //void RenderSegmentDescription2(GameEntity company)
    //{
    //    var audiences = Marketing.GetAudienceInfos();
    //    var worth = Marketing.GetPositioningWorth(company, Positioning);

    //    var a = Positioning.Loyalties
    //        .Select((l, i) => new { i, cost = Marketing.GetAudienceWorth(audiences[i]), isLoyal = l >= 0 });

    //    var favoriteAudiences = a.Where(f => f.isLoyal);
    //    var hatedAudiences = a.Where(f => !f.isLoyal);

    //    var favoriteAudiencesDescription = string.Join("\n", favoriteAudiences.Select(f => DescribeAudience(f.cost, f.i)));
    //    var hatedAudiencesDescription = string.Join("\n", hatedAudiences.Select(f => DescribeAudience(f.cost, f.i)));

    //    SegmentDescription.text = $"{Positioning.name}\n\n<b>Potential Income</b>\n{Visuals.Positive(Format.MinifyMoney(worth))}\n\n<b>Suits</b>\n{favoriteAudiencesDescription}\n\n<b>Hate</b>\n{hatedAudiencesDescription}\n\n";
    //}
}
