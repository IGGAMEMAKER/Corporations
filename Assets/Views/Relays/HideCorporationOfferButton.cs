using Assets.Core;

public class HideCorporationOfferButton : HideOnSomeCondition
{
    public override bool HideIf()
    {
        bool isCorporation = MyCompany.company.CompanyType == CompanyType.Corporation;
        bool isDaughterAlready = Companies.IsRelatedToPlayer(Q, SelectedCompany);

        return !isCorporation || isDaughterAlready;

    }
}
