using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestCampaignButton : TimedButton
{
    int CompanyId;
    public override void Execute()
    {
        var company = Companies.GetCompany(GameContext, CompanyId);

        MarketingUtils.StartTestCampaign(company, GameContext);
    }

    public void SetCompanyId(int companyId)
    {
        CompanyId = companyId;

        ViewRender();
    }

    public override bool HasActiveTimer()
    {
        return Cooldowns.IsHasTask(GameContext, new CompanyTaskMarketingTestCampaign(CompanyId));
    }

    public override bool IsInteractable()
    {
        return !HasActiveTimer();
    }

    public override int QueuedTasks()
    {
        return 0;
    }

    public override string StandardTitle()
    {
        return "Test campaign (+100 clients)";
    }

    public override int TimeRemaining()
    {
        var t = Cooldowns.GetTask(GameContext, new CompanyTaskMarketingTestCampaign(CompanyId));

        return t.EndTime - CurrentIntDate;
    }
}