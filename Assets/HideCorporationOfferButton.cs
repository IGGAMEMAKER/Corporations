using Assets.Utils;

public class HideCorporationOfferButton : HideOnSomeCondition
{
    public override bool HideIf()
    {
        bool isCorporation = MyCompany.company.CompanyType == CompanyType.Corporation;
        bool isDaughterAlready = CompanyUtils.IsCompanyRelatedToPlayer(GameContext, SelectedCompany);

        return !isCorporation || isDaughterAlready;

    }
}
