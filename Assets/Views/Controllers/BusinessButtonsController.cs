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
        bool isProductCompany = Companies.IsProductCompany(SelectedCompany);
        bool isCorporation = MyCompany.company.CompanyType == CompanyType.Corporation;

        bool canBePartnersTheoretically = Companies.IsCanBePartnersTheoretically(MyCompany, SelectedCompany);

        bool isRelatedToPlayer = Companies.IsRelatedToPlayer(Q, SelectedCompany); // CompanyUtils.IsDaughterOfCompany(MyGroupEntity, SelectedCompany) || SelectedCompany == MyCompany;
        bool isDaughter = Companies.IsDaughterOf(MyCompany, SelectedCompany);

        // controlled company buttons

        // is independent company

        Pivot.SetActive(false);


        // daughters
        ManageCompany.SetActive(false);
        SellCompany.SetActive(isDaughter);
        CloseCompany.SetActive(isDaughter);

        // other companies
        AcquireCompany.SetActive(!isRelatedToPlayer);

        JoinCorporation.SetActive(false && !isRelatedToPlayer && isCorporation);

        bool arePartnersAlready = Companies.IsHaveStrategicPartnershipAlready(MyCompany, SelectedCompany);
        SendPartnership.SetActive(!isRelatedToPlayer && (canBePartnersTheoretically || arePartnersAlready));
    }
}
