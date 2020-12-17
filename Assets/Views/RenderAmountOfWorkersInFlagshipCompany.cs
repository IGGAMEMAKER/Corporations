using Assets.Core;

public class RenderAmountOfWorkersInFlagshipCompany : ParameterView
{
    public override string RenderValue()
    {
        return Teams.GetTotalEmployees(Flagship).ToString();
    }
}
