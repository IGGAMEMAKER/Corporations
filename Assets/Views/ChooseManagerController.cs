public class ChooseManagerController : ButtonController
{
    public override void Execute()
    {
        var c = GetComponent<RenderCompanyRoleOrHireWorkerWithThatRole>();
        var role = c.role;

        c.HighlightWorkerRole(true);
        //FindObjectOfType<ManagerTabRelay>().ToggleRole(role);
    }
}
