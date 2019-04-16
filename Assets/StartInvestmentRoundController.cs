using Assets.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartInvestmentRoundController : ButtonController
{
    public override void Execute()
    {
        CompanyUtils.SpawnProposals(GameContext, SelectedCompany.company.Id);
        ReNavigate();
    }
}
