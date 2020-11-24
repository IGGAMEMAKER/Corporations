using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PositioningManagerView : View
{
    public ProductPositioning Positioning;

    public Text SegmentDescription;

    public CompaniesFocusingSpecificSegmentListView CompaniesFocusingSpecificSegmentListView;

    public override void ViewRender()
    {
        base.ViewRender();

        var company = Flagship;

        var positionings = Marketing.GetNichePositionings(company);

        SegmentDescription.text = Positioning.name;

        CompaniesFocusingSpecificSegmentListView.SetSegment(Positioning);
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
