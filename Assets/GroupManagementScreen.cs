public class GroupManagementScreen : View
{
    public CompanyPreviewView CompanyPreviewView;

    public override void ViewRender()
    {
        base.ViewRender();

        CompanyPreviewView.SetEntity(MyGroupEntity);
    }
}
