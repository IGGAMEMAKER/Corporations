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
        var users = Marketing.GetClients(company);

        var usergoal = users >= criticalMass ? Visuals.Positive("COMPLETED") : Visuals.Negative("In progress");

        var marketrequirements = Products.GetMarketDemand(Markets.Get(Q, company));
        var level = Products.GetProductLevel(company);

        var levelgoal = level >= marketrequirements ? Visuals.Positive("COMPLETED") : Visuals.Negative("In progress");

        var goal = Flagship.companyGoal.InvestorGoal;

        var t = $"<b>Goals</b> ({goal})";

        if (goal == InvestorGoal.Prototype)
            t += $"\n\nUpgrade product to 1LVL";

        if (goal >= InvestorGoal.FirstUsers)
            t += $"\n\nGain Critical mass of users: {criticalMass} \n(you have: {users}) {usergoal}";

        if (goal >= InvestorGoal.BecomeMarketFit)
            t += $"\n\nReach market requirements: {marketrequirements}LV \n(you have: {level}LV) {levelgoal}";

        if (Companies.IsReleaseableApp(company, Q))
        {
            t += "\n\n" + Visuals.Positive("You can release your product!!!");
        }

        return t;
    }
}
