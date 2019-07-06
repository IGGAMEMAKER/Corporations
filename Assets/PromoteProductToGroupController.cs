using Assets.Utils;
using UnityEngine;

public class PromoteProductToGroupController : ButtonController
{
    public override void Execute()
    {
        int groupId = CompanyUtils.PromoteProductCompanyToGroup(GameContext, MyProductEntity.company.Id);

        if (groupId == -1)
            return;

        if (MyGroupEntity == null)
            CompanyUtils.SetPlayerControlledCompany(GameContext, groupId);

        NavigateToProjectScreen(groupId);
    }
}
