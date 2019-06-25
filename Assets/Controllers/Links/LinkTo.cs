public class LinkTo : ButtonController
{
    public ScreenMode TargetMenu;

    public override void Execute()
    {
        switch (TargetMenu)
        {
            case ScreenMode.CharacterScreen:
                NavigateToHuman(Me.human.Id);
                break;
            case ScreenMode.IndustryScreen:
                NavigateToIndustry(IndustryType.Search);
                break;
            case ScreenMode.GroupManagementScreen:
                NavigateToCompany(TargetMenu, MyGroupEntity.company.Id);
                break;
            case ScreenMode.NicheScreen:
                NavigateToNiche(NicheType.SearchEngine);
                break;
            case ScreenMode.InvesmentsScreen:
                NavigateToCompany(TargetMenu, SelectedCompany.company.Id);
                break;
            case ScreenMode.InvesmentProposalScreen:
                NavigateToCompany(TargetMenu, MyCompany.company.Id);
                break;
            case ScreenMode.ProjectScreen:
                NavigateToCompany(TargetMenu, SelectedCompany.company.Id);
                break;

            case ScreenMode.TeamScreen:
                NavigateToCompany(TargetMenu, MyProductEntity.company.Id);
                break;
            case ScreenMode.DevelopmentScreen:
                NavigateToCompany(TargetMenu, MyProductEntity.company.Id);
                break;
            case ScreenMode.MarketingScreen:

                NavigateToCompany(TargetMenu, MyProductEntity.company.Id);
                break;

            default:
                Navigate(TargetMenu);
                break;
        }
    }
}
