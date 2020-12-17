using Assets.Core;
using System.Linq;

public class SalaryView : ParameterView
{
    public override string RenderValue()
    {
        var human = SelectedHuman;

        var salary = Teams.GetSalaryPerRating(human);

        if (Humans.IsEmployed(human))
        {
            salary = Humans.GetSalary(human);
        }

        var character = string.Join("\n", human.humanSkills.Traits.Select(t => t.ToString()));

        return $"<b>Salary</b>\n{salary}$";
        //return $"<b>Salary</b>\n{salary}$ \n\n<b>Traits</b>\n{character}";
    }
}
