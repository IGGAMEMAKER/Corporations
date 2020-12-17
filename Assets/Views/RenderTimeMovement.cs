using Assets.Core;
using UnityEngine.UI;

public class RenderTimeMovement : View
{
    public Image Background;

    public override void ViewRender()
    {
        base.ViewRender();

        var dateDescription = Format.GetDateDescription(CurrentIntDate);

        var period = 7f;

        var dayOfMonth = dateDescription.day + 1;

        Background.fillAmount = (dayOfMonth % period) / period;
    }
}
