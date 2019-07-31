using Assets.Utils;
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

    public override void ViewRender()
    {
        base.ViewRender();

        var previousReport = CompanyStatisticsUtils.GetPreviousAnnualReport(GameContext);
        var lastReport = CompanyStatisticsUtils.GetLastAnnualReport(GameContext);

        var groupShareholderId = MyCompany.shareholder.Id;

        var group = lastReport.Groups.Find(r => r.ShareholderId == groupShareholderId);

        var groupPosition = group.position;

        var lastGroupPosition = group.position;



        GroupRating.text = $"#{groupPosition + 1}";
    }

    void RenderHuman(AnnualReport previousReport, AnnualReport currentReport)
    {
        var shareholderId = Me.shareholder.Id;

        var previousList = previousReport.People;

        var human = currentReport.People.Find(r => r.ShareholderId == shareholderId);
        var position = human.position;


        var previousHuman = previousList.Find(r => r.ShareholderId == shareholderId);
        var previousHumanIndex = previousList.FindIndex(r => r.ShareholderId == shareholderId);

        var previousPosition = previousHumanIndex < 0 ? currentReport.People.Count : previousHuman.position;


        ForbesRating.text = $"#{position + 1}";

    }
}
