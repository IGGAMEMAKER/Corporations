using Assets.Utils;

public class HideJoinCorporationIfNecessary : HideOnSomeCondition
{
    public override bool HideIf()
    {
        return !CompanyUtils.IsCompanyWillAcceptCorporationOffer(GameContext, SelectedCompany.company.Id, MyCompany.shareholder.Id);
    }
}
