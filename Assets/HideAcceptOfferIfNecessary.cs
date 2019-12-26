using Assets.Core;

public class HideAcceptOfferIfNecessary : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return !Companies.IsCompanyWillAcceptAcquisitionOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id);
    }
}
