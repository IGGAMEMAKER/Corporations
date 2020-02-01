using Assets.Core;

public class HideAcceptOfferIfNecessary : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return !Companies.IsCompanyWillAcceptAcquisitionOffer(Q, SelectedCompany.company.Id, MyCompany.shareholder.Id);
    }
}
