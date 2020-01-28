﻿using Assets.Core;

public class TestCampaignButton : TimedButton
{
    public override void Execute()
    {
        var company = Companies.Get(GameContext, CompanyId);

        Marketing.StartTestCampaign(company, GameContext);
    }

    public override bool IsInteractable()
    {
        return !HasActiveTimer();
    }

    public override string StandardTitle()
    {
        return "Test campaign (+100 clients)";
    }

    public override CompanyTask GetCompanyTask()
    {
        return new CompanyTaskMarketingTestCampaign(CompanyId);
    }

    public override string ShortTitle()
    {
        return "Test campaign";
    }
}