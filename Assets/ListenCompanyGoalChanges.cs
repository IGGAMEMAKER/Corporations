public class ListenCompanyGoalChanges : Controller
    , IAnyCompanyGoalListener
{
    public override void AttachListeners()
    {
        if (!SelectedCompany.hasAnyCompanyGoalListener)
            SelectedCompany.AddAnyCompanyGoalListener(this);
    }

    public override void DetachListeners()
    {
        if (SelectedCompany.hasAnyCompanyGoalListener)
            SelectedCompany.RemoveAnyCompanyGoalListener(this);
    }

    void IAnyCompanyGoalListener.OnAnyCompanyGoal(GameEntity entity, InvestorGoal investorGoal, int expires, long measurableGoal)
    {
        Render();
    }
}
