using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnnualReportView : View
{
    public Text ForbesRating;
    public Text ForbesRatingChange;

    public Text GroupRating;
    public Text GroupRatingChange;

    public Text CurrentYear;

    public override void ViewRender()
    {
        base.ViewRender();

        var previousReport = CompanyStatisticsUtils.GetPreviousAnnualReport(Q);
        var currentReport = CompanyStatisticsUtils.GetCurrentAnnualReport(Q);

        RenderHuman(previousReport, currentReport);
        RenderGroup(previousReport, currentReport);

        int CurrentIntYear = C.START_YEAR + CurrentIntDate / 360;

        CurrentYear.text = $"Results of year {CurrentIntYear - 1}";
    }

    void RenderHuman(AnnualReport previousReport, AnnualReport currentReport)
    {
        var shareholderId = Hero.shareholder.Id;

        var previousList = previousReport.People;
        var currentList = currentReport.People;

        RenderChanges(previousList, currentList, shareholderId, ForbesRating, ForbesRatingChange);
    }

    void RenderGroup(AnnualReport previousReport, AnnualReport currentReport)
    {
        var shareholderId = MyCompany.shareholder.Id;

        var previousList = previousReport.Groups;
        var currentList = currentReport.Groups;

        RenderChanges(previousList, currentList, shareholderId, GroupRating, GroupRatingChange);
    }


    void RenderChanges(List<ReportData> previousList, List<ReportData> currentList, int shareholderId, Text RatingText, Text RatingChangeText)
    {
        var human = currentList.Find(r => r.ShareholderId == shareholderId);
        var position = human.position;

        RatingText.text = $"#{position + 1}";
        //RatingText.gameObject.GetComponent<RatingAnimation>().SetValue(currentList.Count, position + 1);


        var previousIndex = previousList.FindIndex(r => r.ShareholderId == shareholderId);

        var previousHuman = previousList.Find(r => r.ShareholderId == shareholderId);


        var previousPosition = previousIndex < 0 ? currentList.Count : previousHuman.position;

        var change = position - previousPosition;

        var changeText = Format.Sign(-change) + " per year";
        RatingChangeText.text = Visuals.Colorize(changeText, change <= 0);
    }
}
