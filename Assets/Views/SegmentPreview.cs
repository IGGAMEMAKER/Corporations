using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    public void SetEntity(ProductPositioning positioning)
    {
        var company = Flagship;

        var positionings = Marketing.GetNichePositionings(company);
        SegmentId = Marketing.GetCoreAudienceId(company);
        var info = Marketing.GetAudienceInfos()[SegmentId];



        var worth = Marketing.GetPositioningWorth(company, positioning);

        //bool isTargetAudience = Marketing.IsTargetAudience(company, segmentId);

        bool isOurPositioning = company.productPositioning.Positioning == positioning.ID;
        Title.text = Visuals.Colorize($"{positioning.name} \n<b>{Format.MinifyMoney(worth)}</b>", isOurPositioning ? Colors.COLOR_CONTROL : Colors.COLOR_WHITE);

        var competition = Companies.GetCompetitionInSegment(Flagship, Q, positioning.ID);

        if (competition.Count() == 0)
        {
            Title.text += " " + Visuals.Positive("FREE");
        }


        Icon.texture = Resources.Load<Texture2D>($"Audiences/{info.Icon}");


        //var audienceColor = Visuals.GetColorFromString(isTargetAudience ? Colors.COLOR_GOLD : Colors.COLOR_WHITE);
        //if (isTargetAudience)
        //    PanelImage.color = audienceColor;

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
