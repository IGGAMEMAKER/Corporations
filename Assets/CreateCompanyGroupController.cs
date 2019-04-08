using Assets.Utils;

public class CreateCompanyGroupController : ButtonController
{
    public override void Execute()
    {
        string name = "Company Group #" + CompanyUtils.GenerateCompanyId(GameContext);

        int groupId = CompanyUtils.GenerateCompanyGroup(GameContext, name);

        CompanyUtils.AttachToGroup(GameContext, MyGroupEntity.company.Id, groupId);
    }
}
