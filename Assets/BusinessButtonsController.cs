using Assets.Utils;
using UnityEngine;

public class BusinessButtonsController : View
{
    public GameObject PivotButton;
    [SerializeField] GameObject ManageCompanyButton;
    [SerializeField] GameObject CloseCompanyButton;
    [SerializeField] GameObject SellCompanyButton;
    public GameObject JoinCorporation;

    private void OnEnable()
    {
        var c = SelectedCompany;

        bool isProductCompany = c.company.CompanyType == CompanyType.ProductCompany;

        // controlled company buttons

        // is independent company

        PivotButton.SetActive(c.isControlledByPlayer && isProductCompany);


        bool isMyCompany = c.company.Id == MyCompany.company.Id;
        bool manageable = Companies.IsCompanyRelatedToPlayer(GameContext, SelectedCompany); // CompanyUtils.IsDaughterOfCompany(MyGroupEntity, SelectedCompany) || SelectedCompany == MyCompany;

        ManageCompanyButton.SetActive(false && manageable);
        SellCompanyButton.SetActive(!isMyCompany && manageable);
        CloseCompanyButton.SetActive(!isMyCompany && manageable);

        bool isCorporation = MyCompany.company.CompanyType == CompanyType.Corporation;
        JoinCorporation.SetActive(isCorporation && !manageable);

        //LeaveCEOButton.SetActive(c.isControlledByPlayer);

        //// not controlled
        //NominateAsCEO.SetActive(false && !c.isControlledByPlayer && !isGroupCEOAlready);
    }
}
