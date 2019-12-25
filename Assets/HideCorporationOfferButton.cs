using Assets.Utils;

public class HideCorporationOfferButton : HideOnSomeCondition
{
    public override bool HideIf()
    {
        bool isCorporation = MyCompany.company.CompanyType == CompanyType.Corporation;
        bool isDaughterAlready = Companies.IsRelatedToPlayer(GameContext, SelectedCompany);

        return !isCorporation || isDaughterAlready;

    }
}
