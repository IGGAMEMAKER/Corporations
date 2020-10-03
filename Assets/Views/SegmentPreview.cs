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
        Title.text = $"{info.Name}";

        var have = Marketing.GetClients(Flagship, segmentId);

        var max = Marketing.GetAudienceInfos()[segmentId].Size;
        Description.text = $"{Format.Minify(have)} / {Format.Minify(max)} users";

        SegmentId = segmentId;

        Icon.texture = Resources.Load<Texture2D>($"Audiences/{info.Icon}");

        var company = Flagship;
        bool isTargetAudience = Marketing.IsTargetAudience(company, Q, segmentId);

        var audienceColor = Visuals.GetColorFromString(isTargetAudience ? Colors.COLOR_GOLD : Colors.COLOR_WHITE);
        if (isTargetAudience)
            PanelImage.color = audienceColor;

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
