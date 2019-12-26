using Assets.Core;
using UnityEngine.UI;

public class ScheduleView : View
{
    public ResourceView ScheduleResourceView;
    public Text PauseStatus;

    public override void ViewRender()
    {
        base.ViewRender();

        var dateDescription = Format.GetDateDescription(CurrentIntDate);

        var dateString = Format.FormatDate(CurrentIntDate);

        //var dateString = $"{day + 1:00} {month + 1:MM} {year:0000}";

        //var DateT = new DateTime(year, 1, 1).AddMonths(month).AddDays(day);
        //var dateString = DateT.ToString("dd MMM yyyy");

        ScheduleResourceView.UpdateResourceValue("Date", dateString);

        PauseStatus.gameObject.SetActive(!ScheduleUtils.IsTimerRunning(GameContext));
    }
}
