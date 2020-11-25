using Assets.Core;

public class CreateCompanyGroupController : ButtonController
{
    public override void Execute()
    {
        string name = "Company Group #" + Companies.GenerateCompanyId(Q);

        var group = Companies.GenerateCompanyGroup(Q, name);

        int groupId = group.company.Id;
    }
}
