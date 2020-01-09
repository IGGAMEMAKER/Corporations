using Assets.Core;

public class NavigateToNextProduct : IterateOverCompaniesButtonController
{
    public override GameEntity[] GetEntities()
    {
        return Companies.GetDaughterProductCompanies(GameContext, MyCompany);
    }

    public override ScreenMode GetScreenMode() => ScreenMode.DevelopmentScreen;
}
