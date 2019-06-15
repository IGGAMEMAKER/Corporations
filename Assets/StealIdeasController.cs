using Assets.Utils;

public class StealIdeasController : ButtonController
{
    int targetId;

    public override void Execute()
    {
        var target = CompanyUtils.GetCompanyById(GameContext, targetId);

        ProductUtils.StealIdeas(MyProductEntity, target, GameContext);
    }

    internal void SetTargetCompanyForStealing(int companyId)
    {
        targetId = companyId;
    }
}
