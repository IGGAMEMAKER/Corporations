using Assets.Core;
using System.Linq;

public class RenderProductActivities : ParameterView
{
    public override string RenderValue()
    {
        var tasks = Cooldowns.GetTasksOfCompany(GameContext, SelectedCompany.company.Id);

        var count = tasks.Count();

        var max = 1;

        if (count > 0)
            return $"Current activities ({count} / {max})";
        else
            return "";
    }
}
