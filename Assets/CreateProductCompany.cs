using Assets.Utils;
using Assets.Utils.Formatting;

public class CreateProductCompany : ButtonController
{
    public override void Execute()
    {
        NicheType nicheType = ScreenUtils.GetSelectedNiche(GameContext);

        string name = "New Company " + CompanyUtils.GenerateCompanyId(GameContext);

        var c = CompanyUtils.GenerateProductCompany(GameContext, name, nicheType);

        var companyID = c.company.Id;

        if (MyGroupEntity != null)
        {
            AttachToGroup(companyID);
        }
        else if (MyProductEntity == null)
        {
            SetMyselfAsCEO(companyID);
        }
    }

    void AttachToGroup(int companyID)
    {
        NicheType nicheType = ScreenUtils.GetSelectedNiche(GameContext);
        var startCapital = NicheUtils.GetStartCapital(nicheType, GameContext);

        if (!CompanyUtils.IsEnoughResources(MyCompany, new Assets.Classes.TeamResource(startCapital)))
            return;

        CompanyUtils.SpendResources(MyCompany, startCapital);
        CompanyUtils.AttachToGroup(GameContext, MyGroupEntity.company.Id, companyID);

        name = MyGroupEntity.company.Name + " " + EnumUtils.GetFormattedNicheName(nicheType);
        CompanyUtils.Rename(GameContext, companyID, name);
    }

    void SetMyselfAsCEO(int companyID)
    {
        CompanyUtils.BecomeCEO(GameContext, companyID);
    }
}
