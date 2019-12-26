using Assets.Core;

public class CreateCompanyGroupController : ButtonController
{
    public override void Execute()
    {
        string name = "Company Group #" + Companies.GenerateCompanyId(GameContext);

        int groupId = Companies.GenerateCompanyGroup(GameContext, name).company.Id;

        Companies.AttachToGroup(GameContext, MyGroupEntity.company.Id, groupId);
    }
}
