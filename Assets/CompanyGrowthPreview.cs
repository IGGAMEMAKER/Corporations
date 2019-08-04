using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
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

        var current = CompanyStatisticsUtils.GetCurrentAnnualReport(GameContext);
        var prev = CompanyStatisticsUtils.GetPreviousAnnualReport(GameContext);

        var growthAbsolute = CompanyStatisticsUtils.GetIncomeGrowthAbsolute(entity, 12);
        var growthRelative = CompanyStatisticsUtils.GetIncomeGrowth(entity, 12);
        Value.text = Format.MoneyToInteger(growthAbsolute);

        Value.GetComponent<Hint>().SetHint("");
    }

    public void SetEntity(GameEntity c)
    {
        entity = c;

        Render();
    }
}
