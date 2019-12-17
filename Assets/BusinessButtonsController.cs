using Assets.Utils;
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

        bool isRelatedToPlayer = Companies.IsCompanyRelatedToPlayer(GameContext, SelectedCompany); // CompanyUtils.IsDaughterOfCompany(MyGroupEntity, SelectedCompany) || SelectedCompany == MyCompany;
        bool isDaughter = Companies.IsDaughterOfCompany(MyCompany, SelectedCompany);

        // controlled company buttons

        // is independent company

        Pivot.SetActive(false);


        // daughters
        ManageCompany.SetActive(false);
        SellCompany.SetActive(false && isDaughter);
        CloseCompany.SetActive(isDaughter);

        // other companies
        AcquireCompany.SetActive(!isRelatedToPlayer);

        JoinCorporation.SetActive(!isRelatedToPlayer && isCorporation);

        SendPartnership.SetActive(!isRelatedToPlayer && canBePartnersTheoretically);
    }
}
