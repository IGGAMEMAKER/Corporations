using Assets.Utils;
using System;
using UnityEngine.UI;

public class ScheduleView : View
{
    public ResourceView ScheduleResourceView;
    public Text PauseStatus;

    public override void ViewRender()
    {
        base.ViewRender();

        var internalYear = CurrentIntDate / 360;

        var year = Constants.START_YEAR + internalYear;
        var day = CurrentIntDate % 360;
        var month = day / 30;

        day = day % 30;

        var DateT = new DateTime(year, 1, 1).AddMonths(month).AddDays(day);

        //var dateString = $"{day + 1:00} {month + 1:MM} {year:0000}";
        var dateString = DateT.ToString("dd MMM yyyy");

        ScheduleResourceView.UpdateResourceValue("Date", dateString);

        PauseStatus.gameObject.SetActive(!ScheduleUtils.IsTimerRunning(GameContext));
    }
}
