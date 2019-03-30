using Assets.Utils;

public class CreateCompanyGroupController : ButtonController
{
    public override void Execute()
    {
        GameContext.CreateEntity()
            .AddCompany(CompanyUtils.GenerateCompanyId(GameContext), "New Company Group", CompanyType.Group);
    }
}
