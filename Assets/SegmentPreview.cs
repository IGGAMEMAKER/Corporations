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

    public void SetEntity(AudienceInfo info)
    {
        Title.text = $"{info.Name} <b>{Visuals.PositiveOrNegativeMinified(info.Loyalty)}</b>";
        Description.text = info.Needs;

        Icon.texture = Resources.Load($"Audiences/{info.Icon}") as Texture2D;
    }
}
