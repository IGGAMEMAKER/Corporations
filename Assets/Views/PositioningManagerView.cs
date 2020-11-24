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

    public override void ViewRender()
    {
        base.ViewRender();

        var company = Flagship;

        var positionings = Marketing.GetNichePositionings(company);
        var audiences = Marketing.GetAudienceInfos();

        CompaniesFocusingSpecificSegmentListView.SetSegment(Positioning);
        RenderAudienceChoiceListView.ViewRender();

        var worth = Marketing.GetPositioningWorth(company, Positioning);

        var a = Positioning.Loyalties
            .Select((l, i) => new { i, cost = Marketing.GetAudienceWorth(audiences[i]), isLoyal = l >= 0 });

        var favoriteAudiences = string.Join("\n", a.Where(f => f.isLoyal)
            .Select(f => Visuals.Positive($"{audiences[f.i].Name} ({Format.MinifyMoney(f.cost)})"))
            );

        var hatedAudiences = string.Join("\n", a.Where(f => !f.isLoyal)
            .Select(f => Visuals.Negative($"{audiences[f.i].Name} ({Format.MinifyMoney(f.cost)})"))
            );

        SegmentDescription.text = $"{Positioning.name}\n\n<b>Potential Income</b>\n{Visuals.Positive(Format.MinifyMoney(worth))}\n\n<b>Suits</b>\n{favoriteAudiences}\n\n<b>Hate</b>\n{hatedAudiences}\n\n";
    }

    private void OnEnable()
    {
        ViewRender();
    }

    public void SetAnotherPositioning(ProductPositioning positioning)
    {
        Positioning = positioning;

        ViewRender();
    }
}
