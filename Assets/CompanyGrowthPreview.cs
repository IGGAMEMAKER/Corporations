using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CompanyGrowthPreview : View
{
    public Text CompanyName;
    public LinkToProjectView LinkToProjectView;
    public Text Value;

    public GameEntity entity;

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }

    void Render()
    {
        CompanyName.text = entity.company.Name;
        LinkToProjectView.CompanyId = entity.company.Id;

        //var current = CompanyStatisticsUtils.GetCurrentAnnualReport(GameContext);
        //var prev = CompanyStatisticsUtils.GetPreviousAnnualReport(GameContext);

        //var metrics = MyCompany.metricsHistory.Metrics
        //    .Where(m => CompanyStatisticsUtils.GetLastCalendarYearMetrics(m, CurrentIntDate - 360))
        //    .OrderBy(m => m.Date);

        //Debug.Log("Metrics count: " + metrics.Count());
        //if (metrics.Count() < 11)
        //    return;

        //var current = metrics.Last().Income;

        //var last =  metrics.First().Income;
        //if (last == 0)
        //    last = 1;



        //var growthAbsolute = current - last;
        //var growthRelative = growthAbsolute / last;
        var growthAbsolute = CompanyStatisticsUtils.GetIncomeGrowthAbsolute(entity, 12);
        var growthRelative = CompanyStatisticsUtils.GetIncomeGrowth(entity, 12);

        bool isGrowing = growthAbsolute >= 0;

        Value.text = (isGrowing ? "+" : "") + Format.MoneyToInteger(growthAbsolute);
        Value.color = Visuals.GetColorFromString(isGrowing ? VisualConstants.COLOR_POSITIVE : VisualConstants.COLOR_NEGATIVE);

        Value.GetComponent<Hint>().SetHint($"Income growth: {Format.SignMinified(growthAbsolute)} ({Format.Sign(growthRelative)}%)");
    }

    public void SetEntity(GameEntity c)
    {
        entity = c;

        Render();
    }
}
