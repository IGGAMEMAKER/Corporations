using Assets.Core;
using System.Collections;
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

    List<int> TargetAudiences;

    bool flag = false;

    private void OnEnable()
    {
        var audiences = Marketing.GetAudienceInfos();

        Positioning = Marketing.GetPositioning(Flagship);
        TargetAudiences = audiences.Where(a => Marketing.IsAimingForSpecificAudience(Flagship, a.ID)).Select(a => a.ID).ToList();
        flag = true;

        ShowAll(PivotButtons);
        Hide(PositioningsList);
        Hide(PositioningDescriptionTab);

        ViewRender();
    }

    public override void ViewRender()
    {
        base.ViewRender();

        if (!flag)
            return;

        var company = Flagship;

        var positionings = Marketing.GetNichePositionings(company);
        var audiences = Marketing.GetAudienceInfos();

        CompaniesFocusingSpecificSegmentListView.SetSegment(Positioning);
        RenderAudienceChoiceListView.ViewRender();

        RenderSegmentDescription(company);
    }

    string DescribeAudience(long cost, int i) => Visuals.Negative($"{Marketing.GetAudienceInfos()[i].Name} ({Format.MinifyMoney(cost)})");

    void RenderSegmentDescription2(GameEntity company)
    {
        var audiences = Marketing.GetAudienceInfos();
        var worth = Marketing.GetPositioningWorth(company, Positioning);

        var a = Positioning.Loyalties
            .Select((l, i) => new { i, cost = Marketing.GetAudienceWorth(audiences[i]), isLoyal = l >= 0 });

        var favoriteAudiences = a.Where(f => f.isLoyal);
        var hatedAudiences = a.Where(f => !f.isLoyal);

        var favoriteAudiencesDescription = string.Join("\n", favoriteAudiences.Select(f => DescribeAudience(f.cost, f.i)));
        var hatedAudiencesDescription = string.Join("\n", hatedAudiences.Select(f => DescribeAudience(f.cost, f.i)));

        SegmentDescription.text = $"{Positioning.name}\n\n<b>Potential Income</b>\n{Visuals.Positive(Format.MinifyMoney(worth))}\n\n<b>Suits</b>\n{favoriteAudiencesDescription}\n\n<b>Hate</b>\n{hatedAudiencesDescription}\n\n";
    }

    void RenderSegmentDescription(GameEntity company)
    {
        var audiences = Marketing.GetAudienceInfos();
        var worth = Marketing.GetPositioningWorth(company, Positioning);

        var a = Positioning.Loyalties
            .Select((l, i) => new { i, cost = Marketing.GetAudienceWorth(audiences[i]), isLoyal = l >= 0 });

        var favoriteAudiences   = a.Where(f => f.isLoyal);
        var hatedAudiences      = a.Where(f => !f.isLoyal);

        var favoriteAudiencesDescription = string.Join("\n", favoriteAudiences.Select(f => DescribeAudience(f.cost, f.i)));
        var hatedAudiencesDescription    = string.Join("\n", hatedAudiences.Select(f => DescribeAudience(f.cost, f.i)));

        SegmentDescription.text = $"{Positioning.name}\n\n<b>Potential Income</b>\n{Visuals.Positive(Format.MinifyMoney(worth))}\n\n<b>Suits</b>\n{favoriteAudiencesDescription}\n\n<b>Hate</b>\n{hatedAudiencesDescription}\n\n";
    }

    void RenderAudiences()
    {

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

        FindObjectOfType<RenderAudienceChoiceListView>().SetPositionings(new List<int> { 0, 1, 2, 3 });
    }

    public void OnPivotButton()
    {
        HideButtonDescriptions();
        ShowTabs();

        FindObjectOfType<RenderAudienceChoiceListView>().SetPositionings(new List<int> { 0, 1 });
    }

    public void SetAnotherPositioning(ProductPositioning positioning)
    {
        Positioning = positioning;
        flag = true;

        ViewRender();
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
}
