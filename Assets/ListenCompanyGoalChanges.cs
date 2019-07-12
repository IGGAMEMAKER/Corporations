using UnityEngine;

public class ListenCompanyGoalChanges : Controller
    , ICompanyGoalListener
{
    public override void AttachListeners()
    {
        if (HasProductCompany)
            MyProductEntity.AddCompanyGoalListener(this);
    }

    public override void DetachListeners()
    {
        if (HasProductCompany)
            MyProductEntity.RemoveCompanyGoalListener(this);
    }

    void ICompanyGoalListener.OnCompanyGoal(GameEntity entity, InvestorGoal investorGoal, long measurableGoal)
    {
        Render();
    }
}
