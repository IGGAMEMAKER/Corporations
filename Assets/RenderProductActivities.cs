using Assets.Core;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RenderProductActivities : ParameterView
{
    public override string RenderValue()
    {
        var tasks = Cooldowns.GetTasksOfCompany(GameContext, SelectedCompany.company.Id);

        var count = tasks.Count();

        if (count > 0)
            return $"Current activities ({count})";
        else
            return "";
    }
}
