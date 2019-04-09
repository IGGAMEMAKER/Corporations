using Assets.Utils;

public class AbandonCompanyController : ButtonController
{
    public GameEntity GetCompany()
    {
        int id = (int)MenuUtils.GetMenu(GameContext).menu.Data;

        return CompanyUtils.GetCompanyById(GameContext, id);
    }

    public override void Execute()
    {
        var c = GetCompany();

        if (c != null)
        {
            CompanyUtils.LeaveCEOChair(GameContext, c.company.Id);

            ReNavigate();
        }
    }
}