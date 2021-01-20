using Assets.Core;
using UnityEngine;

public class BusinessButtonsController : View
{
    public GameObject Pivot;
    public GameObject ManageCompany;
    public GameObject CloseCompany;
    public GameObject SellCompany;
    public GameObject JoinCorporation;

    public GameObject AcquireCompany;
    public GameObject SendPartnership;

    private void OnEnable()
    {
        RenderButtons();
    }

    void RenderButtons()
    {
        bool isProductCompany = SelectedCompany.hasProduct;
        bool isCorporation = MyCompany.company.CompanyType == CompanyType.Corporation;

        bool canBePartnersTheoretically = Companies.IsCanBePartnersTheoretically(MyCompany, SelectedCompany);

        bool isDaughter = Companies.IsDaughterOf(MyCompany, SelectedCompany);

        bool isRelatedToPlayer = Companies.IsDirectlyRelatedToPlayer(Q, SelectedCompany);

        bool nonFlagshipDaughter = isDaughter && !SelectedCompany.isFlagship;

        // controlled company buttons

        // is independent company

        Pivot.SetActive(false);


        // daughters
        ManageCompany.SetActive(false);

        Hide(SellCompany);
        Draw(CloseCompany, nonFlagshipDaughter);

        // other companies
        bool possibleToBuy = Companies.IsTheoreticallyPossibleToBuy(Q, MyCompany, SelectedCompany);
        Draw(AcquireCompany, !isRelatedToPlayer && possibleToBuy);

        JoinCorporation.SetActive(false && !isRelatedToPlayer && isCorporation);

        bool arePartnersAlready = Companies.IsHaveStrategicPartnershipAlready(MyCompany, SelectedCompany);
        SendPartnership.SetActive(!isRelatedToPlayer && (canBePartnersTheoretically || arePartnersAlready));
    }
}
