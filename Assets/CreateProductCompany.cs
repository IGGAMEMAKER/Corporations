using Assets.Utils;
using Assets.Utils.Formatting;

public class CreateProductCompany : ButtonController
{
    public override void Execute()
    {
        NicheType nicheType = MenuUtils.GetNiche(GameContext);

        string name = "New Company " + CompanyUtils.GenerateCompanyId(GameContext);

        var c = CompanyUtils.GenerateProductCompany(GameContext, name, nicheType);

        var companyID = c.company.Id;

        if (MyProductEntity == null)
        {
            SetMyselfAsCEO(companyID);
        }
        else if (MyGroupEntity != null)
        {
            AttachToGroup(companyID);

            name = MyGroupEntity.company.Name + " " + EnumFormattingUtils.GetFormattedNicheName(nicheType);

            CompanyUtils.Rename(GameContext, companyID, name);
        }
    }

    void AttachToGroup(int companyID)
    {
        CompanyUtils.AttachToGroup(GameContext, MyGroupEntity.company.Id, companyID);
    }

    void SetMyselfAsCEO(int companyID)
    {
        CompanyUtils.BecomeCEO(GameContext, companyID);
    }
}
