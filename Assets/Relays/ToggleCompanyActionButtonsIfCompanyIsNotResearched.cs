﻿using Assets.Core;

public class ToggleCompanyActionButtonsIfCompanyIsNotResearched : ToggleOnSomeCondition
{
    public override bool Condition()
    {
        var hasTask = Cooldowns.IsHasTask(GameContext, new CompanyTaskExploreCompany(SelectedCompany.company.Id));

        return SelectedCompany.hasResearch || hasTask || Companies.IsRelatedToPlayer(GameContext, SelectedCompany);
    }
}
