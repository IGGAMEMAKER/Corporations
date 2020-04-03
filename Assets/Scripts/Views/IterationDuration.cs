public class IterationDuration : View
{
    public override void ViewRender()
    {
        base.ViewRender();

        GetComponent<RenderConceptProgress>().SetCompanyId(SelectedCompany.company.Id);
    }
}
