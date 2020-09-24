using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SalaryView : ParameterView
{
    public override string RenderValue()
    {
        var human = SelectedHuman;

        var salary = Teams.GetSalaryPerRating(human, Humans.GetRating(human));

        if (Humans.IsEmployed(human))
        {
            var team = Teams.GetTeamOf(human, Q);
            salary = team.Offers[human.human.Id].Salary;
        }

        var character = string.Join("\n", human.humanSkills.Traits.Select(t => t.ToString()));

        return $"<b>Salary</b>\n{salary}$";
        //return $"<b>Salary</b>\n{salary}$ \n\n<b>Traits</b>\n{character}";
    }
}
