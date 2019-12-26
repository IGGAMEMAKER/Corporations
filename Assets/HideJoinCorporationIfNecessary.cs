using Assets.Core;

public class HideJoinCorporationIfNecessary : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return !Companies.IsCompanyWillAcceptCorporationOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id);
    }
}
