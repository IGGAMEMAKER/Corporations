public class LinkToProjectView : ButtonController
{
    public int CompanyId;

    public override void Execute()
    {
        Navigate(ScreenMode.ProjectScreen);
        


        // TODO need to mark clicked project
    }
}
