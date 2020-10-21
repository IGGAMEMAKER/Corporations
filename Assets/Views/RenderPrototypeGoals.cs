using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderPrototypeGoals : ParameterView
{
    public override string RenderValue()
    {
        var company = Flagship;

        if (company.isRelease)
            return "";

        var criticalMass = 500;
        var users = Marketing.GetUsers(company);

        var usergoal = users >= criticalMass ? Visuals.Positive("COMPLETED") : Visuals.Negative("In progress");

        var marketrequirements = Products.GetMarketDemand(Markets.Get(Q, company));
        var level = Products.GetProductLevel(company);

        var levelgoal = level >= marketrequirements ? Visuals.Positive("COMPLETED") : Visuals.Negative("In progress");

        var goal = Flagship.companyGoal.InvestorGoal;

        var t = $"<b>Goals</b> ({goal})";

        if (goal == InvestorGoalType.Prototype)
            t += $"\n\n<i>* Upgrade product to 1LVL</i>";

        if (goal >= InvestorGoalType.FirstUsers)
            t += $"\n\n<i>* Gain Critical mass of users</i>\n\tyou have: {users} / {criticalMass} users {usergoal}";

        if (goal >= InvestorGoalType.BecomeMarketFit)
            t += $"\n\n<i>* Reach market requirements</i>\n\tyou have: {level}LV / {marketrequirements}LV) {levelgoal}";

        if (Companies.IsReleaseableApp(company))
        {
            t += "\n\n" + Visuals.Positive("You can release your product!!!");
        }

        return t;
    }
}
