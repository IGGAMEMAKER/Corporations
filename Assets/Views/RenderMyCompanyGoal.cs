using Assets.Core;

public class RenderMyCompanyGoal : ParameterView
{
    public override string RenderValue()
    {
        return "Goals: " + Investments.GetFormattedCompanyGoals(MyCompany);
    }
}

