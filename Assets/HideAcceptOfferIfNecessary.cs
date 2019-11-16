using Assets.Utils;

public class HideAcceptOfferIfNecessary : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return !CompanyUtils.IsCompanyWillAcceptAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id);
    }
}
