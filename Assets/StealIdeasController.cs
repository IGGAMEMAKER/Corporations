using Assets.Utils;

// TODO REMOVE
public class StealIdeasController : ButtonController
{
    int targetId;

    public override void Execute()
    {
        var target = Companies.GetCompany(GameContext, targetId);
    }

    internal void SetTargetCompanyForStealing(int companyId)
    {
        targetId = companyId;
    }
}
