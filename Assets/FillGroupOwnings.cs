using Assets.Utils;

public class FillGroupOwnings : View
    //, IAnyShareholdersListener
{
    public override void ViewRender()
    {
        base.ViewRender();

        GetComponent<OwningsListView>().SetItems(HasGroupCompany ? GetOwnings() : new GameEntity[0]);
    }

    GameEntity[] GetOwnings()
    {
        return CompanyUtils.GetDaughterCompanies(GameContext, MyGroupEntity.company.Id);
    }
}
