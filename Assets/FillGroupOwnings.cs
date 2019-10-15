using Assets.Utils;

public class FillGroupOwnings : View
    //, IAnyShareholdersListener
{
    public override void ViewRender()
    {
        base.ViewRender();

        GetComponent<OwningsListView>().SetItems(GetOwnings());
    }

    GameEntity[] GetOwnings()
    {
        if (!HasGroupCompany)
            return new GameEntity[0];

        return CompanyUtils.GetDaughterCompanies(GameContext, MyGroupEntity.company.Id);
    }
}
