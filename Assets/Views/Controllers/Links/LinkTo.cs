using Assets.Core;
using UnityEngine;

public class LinkTo : ButtonController
{
    public ScreenMode TargetMenu;

    public override void Execute()
    {
        switch (TargetMenu)
        {
            case ScreenMode.CharacterScreen:
                NavigateToHuman(Hero.human.Id);
                break;

            case ScreenMode.IndustryScreen:
                NavigateToIndustry(IndustryType.Technology);
                break;
            case ScreenMode.NicheScreen:
                NavigateToNiche(SelectedNiche);
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


            case ScreenMode.CompanyGoalScreen:
                NavigateToCompany(TargetMenu, MyCompany.company.Id);
                break;

            case ScreenMode.GroupManagementScreen:
                NavigateToCompany(TargetMenu, MyGroupEntity.company.Id);
                break;
            //case ScreenMode.ManageCompaniesScreen:
            //    var daughters = Companies.GetDaughters(MyGroupEntity, Q);
                
            //    if (daughters.Length > 0)
            //    {
            //        if (Companies.IsDaughterOf(MyCompany, SelectedCompany))
            //        {
            //            Navigate(TargetMenu);
            //            return;
            //        }

            //        // daughters[0].company.Id
            //        Navigate(ScreenMode.ProjectScreen, C.MENU_SELECTED_COMPANY, MyCompany.company.Id);
            //        //if (SelectedCompany.company.Id == MyGroupEntity.company.Id)
            //        //else
            //        //    Navigate(TargetMenu);
            //    }
            //    break;

            case ScreenMode.HoldingScreen:
                ScreenUtils.SetMainPanelId(Q, 0);
                Navigate(TargetMenu);

                break;

            default:
                if (TargetMenu == ScreenMode.ProjectScreen)
                    Debug.Log("NAVIGATE TO PROJECT SCREEN");

                Navigate(TargetMenu);
                break;
        }
    }
}
