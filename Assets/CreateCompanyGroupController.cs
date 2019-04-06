using Assets.Utils;

public class CreateCompanyGroupController : ButtonController
{
    public override void Execute()
    {
        int groupId = CompanyUtils.GenerateCompanyGroup(GameContext, "New Company Group");

        int investorId = CompanyUtils.GetCompanyByName(GameContext, "Alphabet").shareholder.Id;

        CompanyUtils.AddShareholder(GameContext, groupId, investorId, 100);
    }
}
