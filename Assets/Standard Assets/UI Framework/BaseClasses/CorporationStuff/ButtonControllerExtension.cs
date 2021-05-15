using Assets.Core;

// navigation
public abstract partial class ButtonController
{
    public void GoBack()
    {
        ScreenUtils.NavigateBack(Q);
    }

    // navigate
    public void Navigate(ScreenMode screenMode, string field, object data)
    {
        ScreenUtils.Navigate(Q, screenMode, field, data);
    }

    public void Navigate(ScreenMode screenMode)
    {
        ScreenUtils.Navigate(Q, screenMode);
    }

    public void NavigateToTeamScreen(int teamId)
    {
        Navigate(ScreenMode.TeamScreen, C.MENU_SELECTED_TEAM, teamId);
    }

    public void NavigateToMainScreen()
    {
        Navigate(ScreenMode.HoldingScreen);
    }

    public void NavigateToNiche(NicheType niche)
    {
        Navigate(ScreenMode.NicheScreen, C.MENU_SELECTED_NICHE, niche);
    }

    public void NavigateToIndustry(IndustryType industry)
    {
        Navigate(ScreenMode.IndustryScreen, C.MENU_SELECTED_INDUSTRY, industry);
    }

    public void NavigateToCompany(ScreenMode screenMode, int companyId)
    {
        Navigate(screenMode, C.MENU_SELECTED_COMPANY, companyId);
    }

    public void NavigateToHuman(int humanId)
    {
        Navigate(ScreenMode.CharacterScreen, C.MENU_SELECTED_HUMAN, humanId);
    }

    public void NavigateToProjectScreen(int companyId)
    {
        NavigateToCompany(ScreenMode.ProjectScreen, companyId);
    }

    //public void NavigateToMainScreen()
    //{
    //    Navigate(ScreenMode.HoldingScreen);
    //}
}
