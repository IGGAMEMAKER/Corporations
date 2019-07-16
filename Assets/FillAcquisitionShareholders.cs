using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FillAcquisitionShareholders : ListView
{
    public override void SetItem<T>(Transform t, T entity, object data = null)
    {
        int shareholderId = (int)(object)entity;
        t.GetComponent<VotingShareholderView>().SetEntity(shareholderId);

        var investor = CompanyUtils.GetInvestorById(GameContext, shareholderId);

        if (investor.hasHuman)
            t.gameObject.AddComponent<LinkToHuman>().SetHumanId(investor.human.Id);
        else
            t.gameObject.AddComponent<LinkToProjectView>().CompanyId = investor.company.Id;
    }

    void Render()
    {
        SetItems(SelectedCompany.shareholders.Shareholders.Keys.ToArray());
    }

    public override void ViewRender()
    {
        base.ViewRender();

        Render();
    }
}
