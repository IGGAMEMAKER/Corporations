using Assets.Utils;

public class AvailableWorkersView : UpgradedParameterView
{
    public override string RenderHint()
    {
        return "";
    }

    public override string RenderValue()
    {
        return TeamUtils.GetAvailableWorkers(SelectedCompany).ToString();
    }
}
