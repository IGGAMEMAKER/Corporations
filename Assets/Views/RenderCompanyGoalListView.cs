using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderCompanyGoalListView : ListView
{
    public override void SetItem<T>(Transform t, T entity)
    {
        AddIfAbsent<CompanyGoalButtonView>(t.gameObject).SetEntity((InvestorGoal)(object)entity);
        //t.gameObject.AddComponent<CompanyGoalButtonView>().SetEntity((InvestorGoal)(object)entity);
    }

    public override void ViewRender()
    {
        base.ViewRender();

        var goals = new List<InvestorGoal> { InvestorGoal.Prototype, InvestorGoal.FirstUsers, InvestorGoal.BecomeMarketFit };

        SetItems(goals);
    }
}

public class CompanyGoalButtonView : CompanyUpgradeButton
{
    InvestorGoal InvestorGoal;

    public void SetEntity(InvestorGoal goal)
    {
        InvestorGoal = goal;

        ViewRender();
    }

    public override string GetButtonTitle()
    {
        return InvestorGoal.ToString();
    }

    public override string GetBenefits()
    {
        return "???";
    }

    public override bool GetState()
    {
        return true;
    }

    public override string GetHint()
    {
        return "";
    }

    public override void Execute()
    {
        FindObjectOfType<InestmentProposalScreen>().SetGoal(InvestorGoal);
    }
}
