using Assets.Core;
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

    public Image BorderImage;

    public Image DarkPanel;
    public Text Value;

    public SegmentAudiencesListView SegmentAudiencesListView;
    public AudienceListView AudienceListView;

    public int SegmentId;
    public int PositioningID;

    public void SetEntity(ProductPositioning positioning)
    {
        var company = Flagship;

        PositioningID = positioning.ID;

        var positionings = Marketing.GetNichePositionings(company);
        SegmentId = Marketing.GetCoreAudienceId(company);

        var infos = Marketing.GetAudienceInfos();
        var info = infos[SegmentId];

        var actualPositioning = Marketing.GetPositioning(Flagship);

        var selectedPositioning = FindObjectOfType<PositioningManagerView>().Positioning;

        BorderImage.color = Visuals.GetColorFromString(selectedPositioning.ID == positioning.ID ? Colors.COLOR_CONTROL : Colors.COLOR_NEUTRAL);

        var worth = Marketing.GetPositioningWorth(company, positioning);


        bool isOurPositioning = company.productPositioning.Positioning == positioning.ID;

        var positioningColor = isOurPositioning ? Colors.COLOR_CONTROL : Colors.COLOR_WHITE;

        Title.text = $"{positioning.name}\nWorth <size=40><b>{Format.Money(worth, true)}</b></size>";
        Title.color = Visuals.GetColorFromString(positioningColor);

        bool ourPositioning = positioning.ID == actualPositioning.ID;
        if (ourPositioning)
            Title.text += $"\n{Visuals.Colorize("(Our positioning)", Colors.COLOR_CONTROL)}";

        //Title.text = Visuals.Colorize($"{positioning.name}\nWorth <size=40><b>{Format.MinifyMoney(worth)}</b></size>", positioningColor);
        //Title.text = Visuals.Colorize($"{positioning.name} \n<b>{Format.MinifyMoney(worth)}</b>", positioningColor);

        var competition = Companies.GetCompetitionInSegment(Flagship, Q, positioning.ID);

        if (!ourPositioning)
        {
            var competitors = competition.Count();
            var comeptitionColor = Visuals.GetGradientColor(0, 5, competitors, true);

            if (competitors == 0)
            {
                Title.text += "\n" + Visuals.Positive("NO COMPETITION");
            }
            else
            {
                Title.text += "\n" + Visuals.Colorize($"{competitors} companies", comeptitionColor);
            }
        }


        Icon.texture = Resources.Load<Texture2D>($"Audiences/{info.Icon}");

        if (SegmentAudiencesListView != null)
        {
            SegmentAudiencesListView.SetAudiences(positioning);
        }
        //AudienceListView.SetAudiences(positioning.Loyalties.Select((l, i) => new { l, i, asda = infos[i] }).Where(pp => )

        //bool isTargetAudience = Marketing.IsTargetAudience(company, segmentId);
        //var audienceColor = Visuals.GetColorFromString(isTargetAudience ? Colors.COLOR_GOLD : Colors.COLOR_WHITE);
        //if (isTargetAudience)
        //    PanelImage.color = audienceColor;

        HideChanges();
    }

    public void DeselectSegment()
    {
        BorderImage.color = Visuals.GetColorFromString(Colors.COLOR_NEUTRAL);
        Title.color = Visuals.GetColorFromString(Colors.COLOR_NEUTRAL);
    }

    public void ChooseSegment()
    {
        BorderImage.color = Visuals.GetColorFromString(Colors.COLOR_CONTROL);
        Title.color = Visuals.GetColorFromString(Colors.COLOR_CONTROL);
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
