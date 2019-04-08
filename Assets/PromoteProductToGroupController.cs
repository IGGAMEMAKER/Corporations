using Assets.Utils;
using UnityEngine;

public class PromoteProductToGroupController : ButtonController
{
    public override void Execute()
    {
        int groupId = CompanyUtils.PromoteProductCompanyToGroup(GameContext, MyProductEntity.company.Id);

        NavigateToBusinessScreen(groupId);

        Debug.Log("Make a notification maybe?");
    }
}
