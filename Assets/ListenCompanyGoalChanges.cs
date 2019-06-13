using UnityEngine;

public class ListenCompanyGoalChanges : Controller
    , ICompanyGoalListener
{
    public override void AttachListeners()
    {
        MyProductEntity.AddCompanyGoalListener(this);
    }

    public override void DetachListeners()
    {
        MyProductEntity.RemoveCompanyGoalListener(this);
    }

    void ICompanyGoalListener.OnCompanyGoal(GameEntity entity, InvestorGoal investorGoal, int expires, long measurableGoal)
    {
        Debug.Log("OnCompanyGoal");

        Render();
    }
}
