using Assets.Core;

public class CreateCompanyGroupController : ButtonController
{
    public override void Execute()
    {
        string name = "Company Group #" + Companies.GenerateCompanyId(Q);

        int groupId = Companies.GenerateCompanyGroup(Q, name).company.Id;

        Companies.AttachToGroup(Q, MyGroupEntity.company.Id, groupId);
    }
}
