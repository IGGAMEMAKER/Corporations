using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RenderTimeMovement : View
{
    public Image Background;

    public override void ViewRender()
    {
        base.ViewRender();

        var dateDescription = Format.GetDateDescription(CurrentIntDate);

        var dayOfMonth = dateDescription.day + 1;

        Background.fillAmount = dayOfMonth / 30f;
    }
}
