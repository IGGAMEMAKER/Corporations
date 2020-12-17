public class HideJoinCorporationIfNecessary : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return false;
        //return !Companies.IsCompanyWillAcceptCorporationOffer(Q, SelectedCompany, MyCompany.shareholder.Id);
    }
}
