using Assets.Core;

public class HideJoinCorporationIfNecessary : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return !Companies.IsCompanyWillAcceptCorporationOffer(Q, SelectedCompany.company.Id, MyCompany.shareholder.Id);
    }
}
