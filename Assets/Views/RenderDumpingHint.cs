using Assets.Core;

public class RenderDumpingHint : UpgradedParameterView
{
    private int CompanyId;

    public void SetCompanyId(int companyId)
    {
        CompanyId = companyId;
    }

    public override string RenderHint()
    {
        if (dumping)
            return $"This company will get our clients by dropping their prices!\n\nYou need to dump prices too";
        return "";
    }

    bool dumping => Companies.Get(Q, CompanyId).isDumping;

    public override string RenderValue()
    {
        return dumping ? "D" : "";
    }
}
