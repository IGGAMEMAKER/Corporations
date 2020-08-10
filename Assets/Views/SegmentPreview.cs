using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SegmentPreview : View
{
    public Text Title;
    public Text Description;
    public RawImage Icon;
    public Image BorderIcon;
    public Image PanelImage;

    public Image DarkPanel;
    public Text Value;

    public int SegmentId;

    public void SetEntity(AudienceInfo info, int segmentId)
    {
        Title.text = $"{info.Name}"; // <b>{Visuals.PositiveOrNegativeMinified(info.Loyalty)}</b>

        var have = Marketing.GetClients(Flagship, segmentId);

        var max = Marketing.GetAudienceInfos()[segmentId].Amount;
        Description.text = $"{Format.Minify(have)} / {Format.Minify(max)} users"; // info.Needs;

        SegmentId = segmentId;

        Icon.texture = Resources.Load($"Audiences/{info.Icon}") as Texture2D;

        bool isChoicePreview = BorderIcon != null;

        var company = Flagship;
        bool isTargetAudience = company.productTargetAudience.SegmentId == segmentId;

        var audienceColor = Visuals.GetColorFromString(isTargetAudience ? Colors.COLOR_GOLD : Colors.COLOR_WHITE);
        if (isTargetAudience)
            PanelImage.color = audienceColor;

        //if (isChoicePreview)
        //{
        //    BorderIcon.color = audienceColor;

        //    //Description.text = $"{Format.Minify(max)} users (0.05$ each)";
        //    //Title.text = $"{info.Name}";
        //}

        HideChanges();
    }

    public void AnimateChanges(long change)
    {
        Show(DarkPanel);
        Show(Value);

        Value.text = Format.SignOf(change) + Format.Minify(change);
    }

    public void HideChanges()
    {
        if (DarkPanel != null)
            Hide(DarkPanel);

        if (Value != null)
            Hide(Value);
    }
}
