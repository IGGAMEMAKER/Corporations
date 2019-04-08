using Assets.Utils;
using UnityEngine;

public class CreateCompanyGroupController : ButtonController
{
    public override void Execute()
    {
        Debug.Log("Nothing happened in CreateCompanyGroupController");

        return;

        //int newGroupName = CompanyUtils.GenerateCompanyId(GameContext);
        //int groupId = CompanyUtils.GenerateCompanyGroup(GameContext, "Company Group " + newGroupName);

        //int investorId = CompanyUtils.GetCompanyByName(GameContext, "Alphabet").shareholder.Id;

        //CompanyUtils.AddShareholder(GameContext, groupId, investorId, 100);
    }
}
