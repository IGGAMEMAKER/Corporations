using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderLoyaltyChange : ParameterView
{
    public override string RenderValue()
    {
        var text = "";

        var human = SelectedHuman;

        var isEmployed = human.worker.companyId >= 0;
        var company = Companies.Get(Q, human.worker.companyId);


        var playerCulture = Companies.GetActualCorporateCulture(MyCompany, Q);

        var changeBonus = Teams.GetLoyaltyChangeBonus(human, playerCulture, company, Q);
        var change = changeBonus.Sum();

        text += "\n\n";

        bool worksInMyCompany = Humans.IsWorksInCompany(human, MyCompany.company.Id) || Humans.IsWorksInCompany(human, Flagship.company.Id);

        if (isEmployed && worksInMyCompany)
        {
            // TODO copypasted in HumanPreview.cs
            text += Visuals.DescribeValueWithText(change,
                $"Enjoys work in this company!\n\nWeekly loyalty change: +{change}",
                $"Doesn't like this company!\n\nWeekly loyalty change: {change}",
                "Is satisfied by this company"
                );

            text += "\n";

            text += changeBonus.ToString();
        }

        return text;
    }
}
