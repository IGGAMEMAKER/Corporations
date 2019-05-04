﻿public class LinkTo : ButtonController
{
    public ScreenMode TargetMenu;

    public override void Execute()
    {
        switch (TargetMenu)
        {
            case ScreenMode.CharacterScreen:
                Navigate(TargetMenu, null);
                break;
            case ScreenMode.IndustryScreen:
                Navigate(TargetMenu, IndustryType.Search);
                break;
            case ScreenMode.GroupManagementScreen:
                Navigate(TargetMenu, MyGroupEntity.company.Id);
                break;
            case ScreenMode.NicheScreen:
                Navigate(TargetMenu, NicheType.SearchEngine);
                break;
            case ScreenMode.InvesmentsScreen:
                Navigate(TargetMenu, SelectedCompany.company.Id);
                break;
            case ScreenMode.InvesmentProposalScreen:
                Navigate(TargetMenu, SelectedCompany.company.Id);
                break;
            case ScreenMode.ProjectScreen:
                Navigate(TargetMenu, SelectedCompany.company.Id);
                break;

            case ScreenMode.TeamScreen:
                Navigate(TargetMenu, MyProductEntity.company.Id);
                break;
            case ScreenMode.EconomyScreen:
                Navigate(TargetMenu, MyProductEntity.company.Id);
                break;
            case ScreenMode.DevelopmentScreen:
                Navigate(TargetMenu, MyProductEntity.company.Id);
                break;
            default:
                Navigate(TargetMenu, null);
                break;
        }
    }
}
