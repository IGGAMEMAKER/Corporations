public class HideHireManagerLabelIfThereAreNoEmployees : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return Flagship.employee.Managers.Count == 0;
    }
}
