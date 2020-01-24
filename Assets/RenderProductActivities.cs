using Assets.Core;
using System.Linq;

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
