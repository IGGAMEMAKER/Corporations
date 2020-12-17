public class LinkToManagingScreen : ButtonController
{
    public override void Execute()
    {
        if (SelectedCompany == MyGroupEntity)
            Navigate(ScreenMode.GroupManagementScreen);
    }
}
