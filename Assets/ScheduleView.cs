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

        //var internalYear = CurrentIntDate / 360;

        var year = CurrentIntYear; // Constants.START_YEAR + internalYear;
        var day = CurrentIntDate % 360;
        var month = day / 30;

        day = day % 30;


        var dateString = $"{day + 1:00} {GetMonthLiteral(month):000000000} {year:0000}";
        //var dateString = $"{day + 1:00} {month + 1:MM} {year:0000}";

        //var DateT = new DateTime(year, 1, 1).AddMonths(month).AddDays(day);
        //var dateString = DateT.ToString("dd MMM yyyy");

        ScheduleResourceView.UpdateResourceValue("Date", dateString);

        PauseStatus.gameObject.SetActive(!ScheduleUtils.IsTimerRunning(GameContext));
    }

    string GetMonthLiteral(int month)
    {
        switch (month)
        {
            case 0: return "January";
            case 1: return "February";
            case 2: return "March";
            case 3: return "April";
            case 4: return "May";
            case 5: return "June";
            case 6: return "July";
            case 7: return "August";
            case 8: return "September";
            case 9: return "October";
            case 10: return "November";
            case 11: return "December";
            default: return "UNKNOWN MONTH";
        }
    }
}
