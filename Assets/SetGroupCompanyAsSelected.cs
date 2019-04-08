using Assets.Utils;

public class SetGroupCompanyAsSelected : View
{
    private void OnEnable()
    {
        if (MyGroupEntity == null)
        {
            MenuUtils.Navigate(GameContext, ScreenMode.DevelopmentScreen, null);

            return;
        }

        MenuUtils.SetSelectedCompany(MyGroupEntity.company.Id, GameContext);
    }
}
