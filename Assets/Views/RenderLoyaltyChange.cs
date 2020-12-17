using Assets.Core;

public class RenderLoyaltyChange : ParameterView
{
    public override string RenderValue()
    {
        var text = "";

        var human = SelectedHuman;

        var isEmployed = human.worker.companyId >= 0;

        if (!isEmployed)
            return "";

        var company = Companies.Get(Q, human.worker.companyId);

        var culture = Companies.GetActualCorporateCulture(company);

        var changeBonus = Teams.GetLoyaltyChangeBonus(human, company.team.Teams.Find(t => t.Managers.Contains(human.human.Id)), culture, company);
        var change = changeBonus.Sum();

        text += "\n\n";

        bool worksInMyCompany = Humans.IsWorksInCompany(human, MyCompany.company.Id) || Humans.IsWorksInCompany(human, Flagship.company.Id);

        if (worksInMyCompany)
        {
            // TODO copypasted in HumanPreview.cs
            text += Visuals.DescribeValueWithText(change,
                $"Enjoys this company!\nWeekly loyalty change: +{change}",
                $"Doesn't like this company!\nWeekly loyalty change: {change}",
                "Is satisfied by this company"
                );

            text += "\n";

            text += changeBonus.ToString();
        }

        return text;
    }
}
